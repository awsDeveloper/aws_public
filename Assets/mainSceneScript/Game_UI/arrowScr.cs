using UnityEngine;
using System.Collections;

public class arrowScr : MonoBehaviour {
    public int goCount = 0;
    public GameObject[] contents = new GameObject[4];
 
	// Use this for initialization
	void Start () {
        if(PlayerPrefs.HasKey("arrowIndex")){
            int index=PlayerPrefs.GetInt("arrowIndex");
            PlayerPrefs.DeleteKey("arrowIndex");

            while(!contents[index].activeSelf)
                pushed();
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnDestroy()
    {
        int index = -1;
        for (int i = 0; i < contents.Length; i++)
        {
            if (contents[i].activeSelf)
            {
                index = i;
                break;
            }
        }

        if (index < 0)
            return;

        PlayerPrefs.SetInt("arrowIndex", index);
    }


    public void pushed()
    {
        int index = 0;
        for (int i = 0; i < contents.Length; i++)
        {
            if (contents[i].activeSelf)
            {
                index = i;
                break;
            }
        }


        //正なら＋、負ならn-1になるように調整
        int hosei = 1;
        if (goCount < 0)
            hosei = contents.Length - 1;

        int next=(index + hosei) % contents.Length;
        contents[index].SetActive(false);
        contents[next].SetActive(true);

        //親のアクティブ化
        if (next != 3)
            contents[next].transform.parent.gameObject.SetActive(true);
        else
            contents[index].transform.parent.gameObject.SetActive(false);
    }
}


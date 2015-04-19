using UnityEngine;
using System.Collections;

public class arrowScr : MonoBehaviour {
    public int goCount = 0;
    public GameObject[] contents = new GameObject[4];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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


        int next=(index + 1) % contents.Length;
        contents[index].SetActive(false);
        contents[next].SetActive(true);

        if (next <= 2)
            contents[next].transform.parent.gameObject.SetActive(true);
        else
            contents[index].transform.parent.gameObject.SetActive(false);
    }
}


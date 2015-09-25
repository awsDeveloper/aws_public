using UnityEngine;
using System.Collections;

public class getPlaneScrript : MonoBehaviour{
    public bool notGetScr = false;

	bool setFlag = false;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gameObject.transform.parent != null && !setFlag)
		{
			setFlag = true;

			CardScript script = gameObject.transform.parent.gameObject.GetComponent<CardScript> ();

			//check
			string check = script.Text.Replace (" ", "");
			check = check.Replace ("\n", "");
			check = check.Replace ("\r", "");

			//serialNum
            string sNum = script.getParent();//gameObject.transform.parent.gameObject.GetComponent<CardScript> ().SerialNumString;			
			string[] s = sNum.Split ('-');

			//component
            if (check != string.Empty && !notGetScr)
                gameObject.AddComponent(System.Type.GetType(s[0] + "_" + s[1]));
		}
	}

    public bool getSet()
    {
        return setFlag;
    }
}

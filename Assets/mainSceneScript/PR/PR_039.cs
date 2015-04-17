using UnityEngine;
using System.Collections;

public class PR_039 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool upFlag=false;
	bool upFlag2=false;
	
	int size=2000;
	int size2=7000;
	
	// Use this for initialization
	void Start () {
		GameObject Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		GameObject Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
    void Update()
    {
        //always
        if (ManagerScript.getFieldInt(ID, player) == 3 && ManagerScript.getFieldAllNum(7, player) >= 5)
        {
            if (!upFlag)
            {
                upFlag = true;
                ManagerScript.alwaysChagePower(ID, player, size, ID, player);
            }
        }
        else if (upFlag)
        {
            upFlag = false;
            ManagerScript.powChanListChangerClear(ID, player);
        }

        //always 2
        int lrigID = ManagerScript.getLrigID(player);
        if (ManagerScript.getFieldInt(ID, player) == 3 && ManagerScript.getCardLevel(lrigID, player) == 4 && ManagerScript.getLrigType(player) == 5)
        {
            if (!upFlag2)
            {
                upFlag2 = true;
                ManagerScript.changeBasePower(ID, player, 10000);
            }
        }
        else if (upFlag2)
        {
            upFlag2 = false;
            ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower);
        }

        field = ManagerScript.getFieldInt(ID, player);

    }
}

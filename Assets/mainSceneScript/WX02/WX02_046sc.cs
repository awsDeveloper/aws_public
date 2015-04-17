using UnityEngine;
using System.Collections;

public class WX02_046sc : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool upFlag=false;
	public int powerUpValue=4000;
	// Use this for initialization
	void Start () {
		GameObject Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=powerUpValue;
		
		GameObject Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==3 && upCheck()){
            if (!upFlag)
            {
                ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower+powerUpValue);
                upFlag = true;
            }
        }
        else if (upFlag)
        {
            ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower);
            upFlag = false;
        }
    }

	bool upCheck(){
		for(int i=0;i<3;i++){
			int x=ManagerScript.getFieldRankID(3,i,player);
			if(x>=0 && x!=ID){
				int[] c=ManagerScript.getCardClass(x,player);
				if(c[0]==4 && c[1]==0)return true;
			}
		}

		return false;
	}
}

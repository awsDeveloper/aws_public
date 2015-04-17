using UnityEngine;
using System.Collections;

public class WX02_065 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool upFlag=false;
		
	public int powerUpValue=3000;
		
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=powerUpValue;
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		bool flag=false;
		for(int i=0;i<3;i++){
			int x=ManagerScript.getFieldRankID(3,i,player);
			if(x>=0 && x!=ID){
				int[] c=ManagerScript.getCardClass(x,player);
				if(c[0]==2 && c[1]==2)flag=true;
			}
		}
		if(ManagerScript.getFieldInt(ID,player)==3 && flag){
            if (!upFlag)
            {
                ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower + powerUpValue);
                upFlag = true;
            }
        }
        else if (upFlag)
        {
            ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower);
            upFlag = false;
        }	
	}
}

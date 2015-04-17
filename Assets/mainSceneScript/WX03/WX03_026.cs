using UnityEngine;
using System.Collections;

public class WX03_026 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool costFlag=false;
	bool upFlag=false;
	bool equipFlag=false;
		
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.powerUpValue=3000;
	}
	
	// Update is called once per frame
	void Update () {
		//always
		if(field==3 && upCondition()){
            if (!upFlag)
            {
                ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower + BodyScript.powerUpValue);
                upFlag = true;
            }
        }
        else if (upFlag)
        {
            ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower);
            upFlag = false;
        }		
		
		if(field==3 && ManagerScript.getFieldAllNum(9,player)>=7){
			if(!equipFlag && !BodyScript.DoubleCrash){
				BodyScript.DoubleCrash=true;
				equipFlag=true;
			}
		}
		else if(equipFlag){
			equipFlag=false;
			BodyScript.DoubleCrash=false;
		}
				
		field=ManagerScript.getFieldInt(ID,player);	
	}
	
	bool upCondition(){
		int target=player;
		for(int i=0;i<3;i++){
			int x=ManagerScript.getFieldRankID(3,i,target);
			if(x>=0 && ManagerScript.getCardScr(x,target).Name.Contains("手剣　カクマル"))return true;
		}
		
		return false;
	}
}

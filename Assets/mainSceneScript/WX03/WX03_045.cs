using UnityEngine;
using System.Collections;

public class WX03_045 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool costFlag=false;
	
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.powerUpValue=5000;
	}
	
	// Update is called once per frame
	void Update () {
		//ignition
		if(BodyScript.Ignition){	
			BodyScript.Ignition=false;
			
			if(ManagerScript.getIDConditionInt(ID,player)==1){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(8);
				
				int target=player;
				int f=3;
				
				int num=ManagerScript.getFieldAllNum(f,target);
				if(f==3)num=3;				
				
				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(f,i,target);
					if(x>=0){
						BodyScript.effectTargetID.Add(x+50*target);
						BodyScript.effectMotion.Add(34);
					}
				}
			}
		}
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(2);			
		}
		
		//update
		field=ManagerScript.getFieldInt(ID,player);
	}
}

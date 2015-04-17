using UnityEngine;
using System.Collections;

public class WD07_010 : MonoBehaviour {
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
	}
	
	// Update is called once per frame
	void Update () {
		//ignition
		if(BodyScript.Ignition){	
			BodyScript.Ignition=false;
			
			if(ManagerScript.getIDConditionInt(ID,player)==1){
				costFlag=true;
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(45);
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(8);
				
			}
		}
		
		if(costFlag && BodyScript.effectTargetID.Count==0){
			costFlag=false;
			BodyScript.Targetable.Clear();
			
			int target=player;
			int f=0;
			
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getSigniColor(x,target)==5 && ManagerScript.getCardLevel(x,target)<=3){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
		}		
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(player*50);
			BodyScript.effectMotion.Add(2);
		}
		
		//update
		field=ManagerScript.getFieldInt(ID,player);
	}
}

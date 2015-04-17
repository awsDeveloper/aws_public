using UnityEngine;
using System.Collections;

public class WX01_069sc : MonoBehaviour {
	
	//WX03_035
	
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
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
		if(BodyScript.Ignition){	
			BodyScript.Ignition=false;
			int target=player;
			if(ManagerScript.getIDConditionInt(ID,player)==1){
				int handNum=ManagerScript.getFieldAllNum(2,target);
				for(int i=0;i<handNum;i++){
					int x=ManagerScript.getFieldRankID(2,i,target);
					if(x>=0)BodyScript.Targetable.Add(x+50*target);
				}
				if(BodyScript.Targetable.Count>0){
					costFlag=true;
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(19);
					BodyScript.effectTargetID.Add(ID+50*player);
					BodyScript.effectMotion.Add(8);
				}
			}
		}
		
		if(BodyScript.effectTargetID.Count==0 && costFlag){
			costFlag=false;
			BodyScript.Targetable.Clear();
			int target=1-player;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
				if(x>=0 && ManagerScript.getCardPower(x,target)<=5000){
					BodyScript.Targetable.Add(x+(target)*50);
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}
		}
	}
}

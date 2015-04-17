using UnityEngine;
using System.Collections;

public class WX01_083sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool costFlag=false;
	bool IgnitionFlag=false;

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
		if(BodyScript.Ignition && !IgnitionFlag){	
			if(ManagerScript.getIDConditionInt(ID,player)==1){
				int handNum=ManagerScript.getFieldAllNum(2,player);
				for(int i=0;i<handNum;i++){
					int x=ManagerScript.getFieldRankID(2,i,player);
					if(x>=0)BodyScript.Targetable.Add(x+50*player);
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
			if(!BodyScript.effectFlag)BodyScript.Ignition=false;
		}
		
		if(BodyScript.effectTargetID.Count==0 && costFlag){
			costFlag=false;
			BodyScript.effectFlag=true;
			BodyScript.Ignition=false;
			BodyScript.effectTargetID.Add(player*50);
			BodyScript.effectMotion.Add(22);
			BodyScript.effectTargetID.Add(player*50);
			BodyScript.effectMotion.Add(2);
		}
		IgnitionFlag=BodyScript.Ignition;
	}
}

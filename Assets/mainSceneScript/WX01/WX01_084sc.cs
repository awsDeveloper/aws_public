using UnityEngine;
using System.Collections;

public class WX01_084sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool chantFlag_2=false;
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
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(22);
			BodyScript.AntiCheck=true;
			chantFlag_2=true;
			for(int i=0;i<3&&i<ManagerScript.getFieldAllNum(0,player);i++){
				BodyScript.effectTargetID.Add(player*50);
				BodyScript.effectMotion.Add(2);
			}				
		}
		if(BodyScript.effectTargetID.Count==0 && chantFlag_2){			
			chantFlag_2=false;
			if(!BodyScript.AntiCheck){
				int handNum=ManagerScript.getFieldAllNum(2,player);			
				for(int i=0;i<handNum;i++){
					int x=ManagerScript.getFieldRankID(2,i,player);
					if(x>0){
						BodyScript.Targetable.Add(x+50*player);
					}
				}
				if(BodyScript.Targetable.Count>0){
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(19);
				}				
			}
			else BodyScript.AntiCheck=false;
		}
		
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

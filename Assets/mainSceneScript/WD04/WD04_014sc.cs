using UnityEngine;
using System.Collections;

public class WD04_014sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;

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
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,player);
				if(x>0){
					BodyScript.Targetable.Add(x+50*player);
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.TargetIDEnable=true;
				BodyScript.powerUpValue=2000;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(21);
			}
		}
		if(BodyScript.TargetID.Count>0){
			int x=BodyScript.TargetID[0];
			if(ManagerScript.getFieldInt(x%50,x/50)!=3 || ManagerScript.getPhaseInt()==7){
				ManagerScript.upCardPower(x%50,x/50,-BodyScript.powerUpValue);
				BodyScript.TargetID.Clear();
			}
		}
		
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(player*50 );
			BodyScript.effectMotion.Add(2);
		}
		field=ManagerScript.getFieldInt(ID,player);	
	
	}
}

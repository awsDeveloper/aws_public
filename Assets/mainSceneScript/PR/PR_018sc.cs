using UnityEngine;
using System.Collections;

public class PR_018sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool upFlag=false;
	int field=-1;
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=3000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		//always
		if(ManagerScript.getFieldInt(ID,player)==3 && ManagerScript.getTurnPlayer()==player){
			if(!upFlag){
				BodyScript.Power+=BodyScript.powerUpValue;
				upFlag=true;
			}
		}
		else if(upFlag){
			BodyScript.Power-=BodyScript.powerUpValue;;
			upFlag=false;
		}
		//cip
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			int handNum=ManagerScript.getFieldAllNum(2,player);	
			for(int i=0;i<handNum;i++){
				int x=ManagerScript.getFieldRankID(2,i,player);
				if(x>=0){
					BodyScript.Targetable.Add(x+50*player);
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
			}
		}
		///burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(player*50 );
			BodyScript.effectMotion.Add(2);
		}
		field=ManagerScript.getFieldInt(ID,player);	

		
	}
}

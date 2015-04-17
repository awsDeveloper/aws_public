using UnityEngine;
using System.Collections;

public class WD02_007sc : MonoBehaviour {
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
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int handNum=ManagerScript.getFieldAllNum(2,player);
			for(int i=0;i<handNum;i++){
				int x=ManagerScript.getFieldRankID(2,i,player);
				if(x>0){
					BodyScript.Targetable.Add(x+50*player);
				}
			}
			if(BodyScript.Targetable.Count>=3){
				costFlag=true;
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(7);
				BodyScript.effectMotion.Add(7);
				BodyScript.effectMotion.Add(7);
			}
			else BodyScript.Targetable.Clear();
		}
		if(!BodyScript.effectFlag && costFlag){
			BodyScript.effectFlag=true;
			costFlag=false;
			for(int i=0;i<6;i++){
				int x=ManagerScript.getFieldRankID(3,i%3,i/3);
				if(x>0){
					BodyScript.effectTargetID.Add(x+i/3*50);
				}
			}
			
			for(int i=0;i<BodyScript.effectTargetID.Count;i++){
				BodyScript.effectMotion.Add(5);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);		
	}
}

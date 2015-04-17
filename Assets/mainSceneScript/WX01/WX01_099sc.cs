using UnityEngine;
using System.Collections;

public class WX01_099sc : MonoBehaviour {
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
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int enaNum=ManagerScript.getFieldAllNum(6,player);
			for(int i=0;i<enaNum;i++){
				int x=ManagerScript.getFieldRankID(6,i,player);
				if(x>=0 && ManagerScript.getCardType(x,player)==2)BodyScript.Targetable.Add(x+50*player);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(6);
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(20);				
			}
		}
		
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			int enaNum=ManagerScript.getFieldAllNum(6,player);
			for(int i=0;i<enaNum;i++){
				int x=ManagerScript.getFieldRankID(6,i,player);
				if(x>=0)BodyScript.Targetable.Add(x+50*player);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(16);
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(20);				
			}
		}
		field=ManagerScript.getFieldInt(ID,player);
	}
}

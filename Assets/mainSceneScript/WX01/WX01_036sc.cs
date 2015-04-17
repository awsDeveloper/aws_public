using UnityEngine;
using System.Collections;

public class WX01_036sc : MonoBehaviour {
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
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3){
			BodyScript.effectFlag=true;
			int deckNum=ManagerScript.getFieldAllNum(0,player);
			int moveID=ManagerScript.getFieldRankID(0,deckNum-1,player);
			BodyScript.effectTargetID.Add(moveID + 50*player);
			BodyScript.effectMotion.Add(14);
			if(ManagerScript.getCardLevel(moveID,player)<=2 && ManagerScript.getFieldAllNum(3,player)==1 
				&& ManagerScript.getCardType(moveID,player)==2){
				BodyScript.effectTargetID.Add(moveID + 50*player);
				BodyScript.effectMotion.Add(6);
			}
		}
		
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			int deckNum=ManagerScript.getFieldAllNum(0,player);	
			for(int i=0;i<deckNum;i++){
				int x=ManagerScript.getFieldRankID(0,i,player);
				if(x>=0 && ManagerScript.getCardColor(x,player)==1 
					&& ManagerScript.getCardType(x,player)==2 && ManagerScript.getCardLevel(x,player)==4){
					BodyScript.Targetable.Add(x+50*player);
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
		}

		field=ManagerScript.getFieldInt(ID,player);
	}
}

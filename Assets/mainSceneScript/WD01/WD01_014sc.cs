using UnityEngine;
using System.Collections;

public class WD01_014sc : MonoBehaviour {
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
			for(int i=0;i<3;i++){
				int deckNum=ManagerScript.getFieldAllNum(0,player);
				int moveID=ManagerScript.getFieldRankID(0,deckNum-1-i,player);
				BodyScript.effectTargetID.Add(moveID + 50*player);
				BodyScript.effectMotion.Add(14);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);
	}
}

﻿using UnityEngine;
using System.Collections;

public class WX02_053sc : MonoBehaviour {
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
			int target=1-player;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
				if(x>0 && ManagerScript.getCardPower(x,target)<=2000){
					BodyScript.Targetable.Add(x+50*(target));
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
}
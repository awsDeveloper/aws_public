﻿using UnityEngine;
using System.Collections;

public class WX01_093sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;

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
			if(ManagerScript.getIDConditionInt(ID,player)==1){
				int handNum=ManagerScript.getFieldAllNum(2,player);
				for(int i=0;i<handNum;i++){
					int x=ManagerScript.getFieldRankID(2,i,player);
					if(x>=0)BodyScript.Targetable.Add(x+50*player);
				}
				if(BodyScript.Targetable.Count>0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(19);
					BodyScript.effectTargetID.Add(ID+50*player);
					BodyScript.effectMotion.Add(8);
					BodyScript.effectTargetID.Add(player*50);
					BodyScript.effectMotion.Add(26);
				}
			}
			if(!BodyScript.effectFlag)BodyScript.Ignition=false;
		}		
	}
}

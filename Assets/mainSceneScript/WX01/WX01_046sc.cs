﻿using UnityEngine;
using System.Collections;

public class WX01_046sc : MonoBehaviour {
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
		//useLimit
		BodyScript.useLimit = ManagerScript.getFieldAllNum (2, 1 - player) != 0;

		//effect
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag ){
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,1-player);
				if(x>=0){
					BodyScript.Targetable.Add(x+50*(1-player));
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

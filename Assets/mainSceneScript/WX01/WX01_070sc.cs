﻿using UnityEngine;
using System.Collections;

public class WX01_070sc : MonoBehaviour {
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
			int target=player;
			int f=2;
			int num=ManagerScript.getFieldAllNum(f,target);	
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
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

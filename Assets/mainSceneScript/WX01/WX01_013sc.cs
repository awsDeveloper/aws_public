﻿using UnityEngine;
using System.Collections;

public class WX01_013sc : MonoBehaviour {
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
		if(ManagerScript.getFieldInt(ID,player)==4 && field!=4){
			int handNum=ManagerScript.getFieldAllNum(2,player);
			if(handNum>0){
				for(int i=0;i<handNum;i++){	
					int x=ManagerScript.getFieldRankID(2,i,player);
					BodyScript.Targetable.Add(x+50*player);
				}
				if(BodyScript.Targetable.Count>0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(7);
					BodyScript.effectTargetID.Add(50*player);
					BodyScript.effectMotion.Add(2);
				}
			}
		}
		field=ManagerScript.getFieldInt(ID,player);
	}
}

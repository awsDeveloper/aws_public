﻿using UnityEngine;
using System.Collections;

public class WX01_054sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool upFlag=false;
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
		if(ManagerScript.getFieldInt(ID,player)==3 && ManagerScript.getTurnPlayer()!=player){
			if(!upFlag){
				ManagerScript.changeBasePower(ID,player,18000);
				upFlag=true;
			}
		}
		else if(upFlag){
            ManagerScript.changeBasePower(ID, player, 12000);
            upFlag = false;
		}
	
	}
}

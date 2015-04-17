﻿using UnityEngine;
using System.Collections;

public class WX01_049sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool costFlag=false;
	bool IgnitionFlag=false;

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
		if(BodyScript.Ignition && !IgnitionFlag){	
			if(ManagerScript.getIDConditionInt(ID,player)==1){
				costFlag=true;
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(8);
			}
			else BodyScript.Ignition=false;
		}
		
		if(BodyScript.effectTargetID.Count==0 && costFlag){
			costFlag=false;
			BodyScript.Ignition=false;
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(22);
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(26);
		}
		IgnitionFlag=BodyScript.Ignition;
	}
}

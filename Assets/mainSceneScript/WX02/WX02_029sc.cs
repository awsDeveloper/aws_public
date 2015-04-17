﻿using UnityEngine;
using System.Collections;

public class WX02_029sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool costFlag=false;
	
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
		//cip
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			int target=player;
			int f=2;
			int num=ManagerScript.getFieldAllNum(f,target);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(checkClass(x,target)){
					BodyScript.Targetable.Add(x+50*target);
				}		
			}
			bool flag=false;
			f=0;
			num=ManagerScript.getFieldAllNum(f,target);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardType(x,target)==2)flag=true;
			}
			if(BodyScript.Targetable.Count>0 && flag){
				BodyScript.DialogFlag=true;
			}
			else BodyScript.TargetID.Clear();
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
				costFlag=true;
			}
			else BodyScript.Targetable.Clear();
			
			BodyScript.messages.Clear();
		}		
		
		//after cost
		if(costFlag && BodyScript.effectTargetID.Count==0){
			costFlag=false;
			BodyScript.Targetable.Clear();
			int target=player;
			int f=0;
			int num=ManagerScript.getFieldAllNum(f,target);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardType(x,target)==2)BodyScript.Targetable.Add(x+50*target);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}			
		}
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(26);
		}
		field=ManagerScript.getFieldInt(ID,player);
	}
    bool checkClass(int x, int cplayer)
    {
        if (ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_アーム))
            return true;

        return ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_ウェポン);
    }
}

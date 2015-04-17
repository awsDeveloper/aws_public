﻿using UnityEngine;
using System.Collections;

public class WX02_037sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool chantFlag=false;
	int tIDbuf=-1;
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
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int target=player;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
				if(checkClass(x,target)){
					BodyScript.Targetable.Add(x+50*(target));
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(22);
				BodyScript.AntiCheck=true;
				chantFlag=true;
			}
		}
		if(chantFlag && BodyScript.effectTargetID.Count==0){
			chantFlag=false;
			if(!BodyScript.AntiCheck && tIDbuf>=0){
				BodyScript.Targetable.Clear();
				for(int i=0;i<2 && i<ManagerScript.getFieldAllNum(0,player);i++){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(player*50);
					BodyScript.effectMotion.Add(2);
				}			
			}
			else BodyScript.AntiCheck=false;
		}
	
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(player*50);
			BodyScript.effectMotion.Add(2);
			
			bool flag=false;
			int target=player;
			
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
				if(checkClass(x,target)){
					flag=true;
				}
			}
			if(flag){
				BodyScript.effectTargetID.Add(player*50);
				BodyScript.effectMotion.Add(2);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);
		if(BodyScript.effectTargetID.Count>0 && BodyScript.effectMotion[0]!=22){			
			tIDbuf=BodyScript.effectTargetID[0];
		}
	}
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return (c[0]==5 && c[1]==0 );
	}
}
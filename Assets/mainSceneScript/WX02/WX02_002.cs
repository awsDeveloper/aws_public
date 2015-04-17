using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX02_002 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	List<int> equipList=new List<int>();
	int LifeClothNum=0;
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=3000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		//cip
		if(ManagerScript.getFieldInt(ID,player)==4 && field!=4 && !BodyScript.BurstFlag){
			//crash
			int target=player;
			int num=ManagerScript.getFieldAllNum(5,target);
			int x=ManagerScript.getFieldRankID(5,num-1,target);
			if(x>=0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(x+50*target);
				BodyScript.effectMotion.Add(28);
			}
			
			//equip
			num=ManagerScript.getFieldAllNum(5,target);
			for(int i=0;i<num;i++){
				x=ManagerScript.getFieldRankID(5,i,target);
				equip(x,target);				
			}
			
		}
		//pig
		if(ID!=ManagerScript.getLrigID(player)){
			while(equipList.Count>0){
				dequip(0);
			}
		}
		//equip check
		if(ManagerScript.getFieldAllNum(5,player)>LifeClothNum && ID==ManagerScript.getLrigID(player)){
			int x=ManagerScript.getFieldRankID(5,LifeClothNum,player);
			equip(x,player);
		}
		for(int i=0;i<equipList.Count;i++){
			int x=equipList[i]%100;
			if(ManagerScript.getFieldInt(x%50,x/50)!=5){
				if(ManagerScript.getFieldInt(x%50,x/50)==8){
					CardScript cs=ManagerScript.getCardScr(x%50,x/50);
					cs.effectFlag=true;
					cs.effectTargetID.Add(50*player);
					cs.effectMotion.Add(26);
				}
				dequip(i);
				i--;
			}
		}
		
		//ignition
		if(BodyScript.Ignition){
			BodyScript.Ignition=false;

			if(ManagerScript.getLrigConditionInt(player) != 1)
                return;

			int target=1-player;
			int num=ManagerScript.getFieldAllNum(5,target);
			int x=ManagerScript.getFieldRankID(5,num-1,target);
			if(x>=0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+player);
				BodyScript.effectMotion.Add(8);
				BodyScript.effectTargetID.Add(x+50*target);
				BodyScript.effectMotion.Add(28);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);
		LifeClothNum=ManagerScript.getFieldAllNum(5,player);
	}
	
	void equip(int x,int eplayer){
		if(x<0)return;
		CardScript sc=ManagerScript.getCardScr(x,eplayer);
		if(sc.BurstIcon!=1){
			sc.BurstIcon=1;
			equipList.Add(x+50*eplayer);
		}
		else{
			equipList.Add(x+50*eplayer+100);
		}
	}
	void dequip(int index){
		if(index>=equipList.Count)return;
		int x=equipList[index];
		if(x<100 && x>=0){
			CardScript sc=ManagerScript.getCardScr(x%50,x/50);
			sc.BurstIcon=0;
		}
		equipList.RemoveAt(index);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WD07_018 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool DialogFlag=false;
	const int selectMax=1;
	List<int> checkID=new List<int>();
	bool chantFlag=false;
	bool effectFlag=false;
	bool burstEffect=false;
	
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.checkStr.Add("サーチ");
		BodyScript.checkStr.Add("パワーダウン");
		
		for(int i=0;i<BodyScript.checkStr.Count;i++){
			BodyScript.checkBox.Add(false);		
		}
		
		BodyScript.powerUpValue=-8000;
		BodyScript.DialogCountMax=1;
		BodyScript.DialogNum=2;
	}
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			burstEffect=false;
			if(checkID.Count==0){
				BodyScript.DialogFlag=true;
				BodyScript.checkStr[0]="サーチ";
				BodyScript.checkStr[1]="パワーダウン";
			}
			else{
				effect_1();
				effect_2();
			}
		}
		
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			burstEffect=true;
			BodyScript.DialogFlag=true;
			BodyScript.checkStr[0]="リクルート";
			BodyScript.checkStr[1]="蘇生";
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				if(!burstEffect){
					for(int i=0;i<BodyScript.checkBox.Count;i++){
						if(BodyScript.checkBox[i])checkID.Add(i);
					}
					chantFlag=true;
				}
				effect_1();
				effect_2();
			}
			else destory();
			
			BodyScript.messages.Clear();
		}
		
		
		if(BodyScript.effectTargetID.Count==0 && effectFlag){
			effectFlag=false;
			if(!BodyScript.AntiCheck){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
			else BodyScript.AntiCheck=false;
		}
		
		if(BodyScript.effectTargetID.Count==0 && chantFlag){
			chantFlag=false;
			destory();
		}
		
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	void effect_1(){
		if(BodyScript.checkBox[0]){
			if(burstEffect){
				int num=ManagerScript.getFieldAllNum(0,player);
				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(0,i,player);
					if(x>=0 && ManagerScript.getSigniColor(x,player)==1)
						BodyScript.Targetable.Add(x+50*player);
				}
				if(BodyScript.Targetable.Count>0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(-2);
					BodyScript.effectMotion.Add(6);
				}
			}
			else{
				int num=ManagerScript.getFieldAllNum(0,player);
				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(0,i,player);
					if(x>=0 && ManagerScript.getCardType(x,player)==2)
						BodyScript.Targetable.Add(x+50*player);
				}
				if(BodyScript.Targetable.Count>0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(50*player);
					BodyScript.effectMotion.Add(22);
					BodyScript.AntiCheck=true;
					effectFlag=true;
				}
			}
		}
	}
	void effect_2(){
		if(BodyScript.checkBox[1]){
			if(burstEffect){
				int f=7;
				int target=player;
				int num=ManagerScript.getNumForCard(f,target);
				
				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(f,i,target);
					if(x>=0 && ManagerScript.getSigniColor(x,target)==5)BodyScript.Targetable.Add(x+50*target);
				}
				
				if(BodyScript.Targetable.Count>0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(-2);
					BodyScript.effectMotion.Add(6);
				}
			}
			else{
				int f=3;
				int target=1-player;
				int num=ManagerScript.getNumForCard(f,target);
				
				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(f,i,target);
					if(x>=0)BodyScript.Targetable.Add(x+50*target);
				}
				
				if(BodyScript.Targetable.Count>0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(34);
				}
			}
		}
	}
	
	void destory(){
		while(checkID.Count>0){
			BodyScript.checkBox[checkID[0]]=false;
			checkID.RemoveAt(0);
		}
	}
}

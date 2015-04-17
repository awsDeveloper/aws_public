using UnityEngine;
using System.Collections;

public class WX03_020 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool costFlag=false;
	bool upFlag=false;
	
	// Use this for initialization
	void Start () {
		GameObject Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		GameObject Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.powerUpValue=2000;
		
		BodyScript.checkStr.Add("蘇生");
		BodyScript.checkStr.Add("バニッシュ");
		for(int i=0;i<BodyScript.checkStr.Count;i++){
			BodyScript.checkBox.Add(false);		
		}
	}
	
	// Update is called once per frame
	void Update () {
		//always
		int tsID=ManagerScript.getTrashSummonID();
		if(ManagerScript.getFieldInt(ID,player)==3 && tsID>=0 && tsID/50==player){
			upFlag=true;
		}
		
		if(upFlag && !BodyScript.effectFlag){
			upFlag=false;
			BodyScript.effectFlag=true;
			
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,player);
				if(x>=0){
					BodyScript.effectTargetID.Add(x+50*player);
					BodyScript.effectMotion.Add(34);
				}
			}
		}
		
		
		//cip
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3){
			BodyScript.Cost[0]=0;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=2;
			
			if(ManagerScript.checkCost(ID,player)){
				BodyScript.DialogFlag=true;
				BodyScript.DialogNum=0;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.BurstFlag){//burst
				if(BodyScript.messages[0].Contains("Yes")){
					effect_1();
					effect_2();
				}
			}
			else{//cip
				if(BodyScript.messages[0].Contains("Yes")){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(ID+player*50);
					BodyScript.effectMotion.Add(17);
					costFlag=true;
				}
			}
			
			BodyScript.messages.Clear();
		}
		
		//cip after cost
		if(BodyScript.effectTargetID.Count==0 && costFlag){
			costFlag=false;
			int f=7;
			int target=player;
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(ManagerScript.getCardType(x,target)==2)BodyScript.Targetable.Add(x+50*target);
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(6);
			}
		}
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			BodyScript.DialogFlag=true;
			BodyScript.DialogNum=2;
			BodyScript.DialogCountMax=1;
		}
		
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	void effect_1(){
		if(BodyScript.checkBox[0]){
			int f=7;
			int target=player;
			int num=ManagerScript.getFieldAllNum(f,target);
			if(f==3)num=3;
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && checkClass(x,target)){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(6);
			}
		}
	}
	
	void effect_2(){
		if(BodyScript.checkBox[1]){
			int f=3;
			int target=1-player;
			int num=ManagerScript.getFieldAllNum(f,target);
			if(f==3)num=3;
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardLevel(x,target)<=2){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}
		}
	}
	
	bool checkClass(int x,int target){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,target);
		return (c[0]==3 && c[1]==1 );
	}
}


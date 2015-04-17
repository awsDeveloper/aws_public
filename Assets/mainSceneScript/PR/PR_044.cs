using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PR_044 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	const int selectMax=2;
	List<int> checkID=new List<int>();
	bool chantFlag=false;
	
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		string[] checkStr=new string[3];
		checkStr[0]="メツム";
		checkStr[1]="蘇生";
		checkStr[2]="パワーダウン";
		for(int i=0;i<3;i++){
			BodyScript.checkStr.Add(checkStr[i]);
		}
		
		for(int i=0;i<BodyScript.checkStr.Count;i++){
			BodyScript.checkBox.Add(false);		
		}
		
	}
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			if(checkID.Count==0){
				BodyScript.DialogFlag=true;
				BodyScript.DialogCountMax=1;
				BodyScript.DialogNum=2;
			}
			else{
				effect_1();
				effect_2();
				effect_3();
				effect_4();
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				for(int i=0;i<BodyScript.checkBox.Count;i++){
					if(BodyScript.checkBox[i])checkID.Add(i);
				}
				chantFlag=true;
				effect_1();
				effect_2();
				effect_3();
				effect_4();
			}
			else {
				destory();
			}
			
			BodyScript.messages.Clear();
		}
		
		//down chant flag
		if(BodyScript.effectTargetID.Count==0 && chantFlag){
			chantFlag=false;
			destory();
		}
		
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	void effect_1(){
		if(BodyScript.checkBox[0]){
			int target=player;
			int f=0;
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<num && i<7;i++){
				int x=ManagerScript.getFieldRankID(f,num-1-i,target);
				if(x>=0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(7);
				}
			}
			
			target=1-player;
			num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<num && i<7;i++){
				int x=ManagerScript.getFieldRankID(f,num-1-i,target);
				if(x>=0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(7);
				}
			}
			
		}
	}
	void effect_2(){
		if(BodyScript.checkBox[1]){
			int target=player;
			int f=7;
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardLevel(x,target)<=3 && checkClass(x,target)){
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
	void effect_3(){
		if(BodyScript.checkBox[2]){
			if(ManagerScript.getFieldAllNum(7,player)>=20)BodyScript.powerUpValue=-8000;
			else if(ManagerScript.getFieldAllNum(7,player)>=10)BodyScript.powerUpValue=-5000;
			else BodyScript.powerUpValue=0;
			
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,1-player);
				if(x>=0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(x+50*(1-player));
					BodyScript.effectMotion.Add(34);					
				}
			}
		}
	}
	void effect_4(){
/*		if(BodyScript.checkBox[3]){
			for(int i=0;i<2 && i<ManagerScript.getFieldAllNum(0,player);i++){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(2);
			}
		}*/
	}
	
	bool checkClass(int x,int target){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,target);
		return (c[0]==3 && c[1]==1 );
	}
	
	void destory(){
		while(checkID.Count>0){
			BodyScript.checkBox[checkID[0]]=false;
			checkID.RemoveAt(0);
		}
	}
}

using UnityEngine;
using System.Collections;

public class WD07_012 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool costFlag=false;
	bool[] levelArray=new bool[4];
	int count=0;
	bool effectFlag=true;
	
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		 BodyScript.powerUpValue=-10000;
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			BodyScript.Cost[0]=0;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=1;
			
			if(ManagerScript.checkCost(ID,player) && checkTrash()>=3){
				 BodyScript.DialogFlag=true;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				costFlag=true;
			}
			
			BodyScript.messages.Clear();
		}
		
		//after cost
		if(costFlag && BodyScript.effectTargetID.Count==0){
			costFlag=false;
			if(checkTrash()>=4)effect();
		}
		
		//after effect
		if(effectFlag && BodyScript.effectTargetID.Count==0){
			effectFlag=false;
			BodyScript.Targetable.Clear();
			
		}
		
		//入力
		if(BodyScript.TargetID.Count>0 ){
			count++;
			
			int id=BodyScript.TargetID[0];
			BodyScript.TargetID.Clear();
			BodyScript.TargetIDEnable=false;
			
			levelArray[ManagerScript.getCardLevel(id%50,id/50)-1]=true;
			
			if(count<4){
				effect();					
			}
			else effectFlag=true;
		}
		
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			burstEffect();
		}
		
		field=ManagerScript.getFieldInt(ID,player);	
	}
	
	void effect(){
		BodyScript.Targetable.Clear();
		
		int target=player;
		int f=7;
		int num=ManagerScript.getFieldAllNum(f,target);
		
		for(int i=0;i<num;i++){
			int x=ManagerScript.getFieldRankID(f,i,target);
			if(x>=0 && checkArray(x,target)){
				BodyScript.Targetable.Add(x+50*target);
			}
		}
		
		if(BodyScript.Targetable.Count>0){
			BodyScript.effectTargetID.Add(-2);
			BodyScript.effectMotion.Add(48);
			BodyScript.TargetIDEnable=true;
		}	
	}
	
	bool checkArray(int x,int target){
		int lev=ManagerScript.getCardLevel(x,target);
		if(lev<0)return false;
		return !levelArray[lev-1];
	}
	
	void reset(){
		count=0;
		for(int i=0;i<levelArray.Length;i++)levelArray[i]=false;
	}
	
	int checkTrash(){
		bool[] lev=new bool[4];
		
		int target=player;
		int f=7;
		int num=ManagerScript.getFieldAllNum(f,target);
		int c=0;
		
		for(int i=0;i<num;i++){
			int x=ManagerScript.getFieldRankID(f,i,target);
			if(x>=0){
				int level=ManagerScript.getCardLevel(x,target);
				if(level>0 && !lev[level-1]){
					lev[level-1]=true;
					c++;
				}					
			}
		}
		
		return c;
	}
	
	void burstEffect(){
		int target=1-player;
		int f=3;
		int num=ManagerScript.getNumForCard(f,target);
		
		for(int i=0;i<num;i++){
			int x=ManagerScript.getFieldRankID(f,i,target);
			if(x>=0){
				BodyScript.Targetable.Add(x+50*target);
			}
		}
		
		if(BodyScript.Targetable.Count>0){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(-1);
			BodyScript.effectMotion.Add(34);
		}		
	}
}

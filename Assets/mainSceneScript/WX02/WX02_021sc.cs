using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX02_021sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
//	bool DialogFlag=false;
	bool costFlag=false;
	bool burstEffect=false;
	List<int> effecterList=new List<int>();
	string sss="";
	
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
		untachable();
		//cip
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			int cost=ManagerScript.getEnaColorNum(1,player);
			cost+=ManagerScript.MultiEnaNum(player);
			int deckNum=ManagerScript.getFieldAllNum(0,player);	
			for(int i=0;i<deckNum;i++){
				int x=ManagerScript.getFieldRankID(0,i,player);
				if(x>=0 && checkClass(x,player)){
					BodyScript.Targetable.Add(x+50*player);
				}
			}
			if(BodyScript.Targetable.Count>0 && cost>=2){
//				ManagerScript.stopFlag=true;
//				DialogFlag=true;
				BodyScript.DialogFlag=true;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				BodyScript.Cost[0]=0;
				BodyScript.Cost[1]=2;
				BodyScript.Cost[2]=0;
				BodyScript.Cost[3]=0;
				BodyScript.Cost[4]=0;
				BodyScript.Cost[5]=0;
				costFlag=true;
			}
			else BodyScript.Targetable.Clear();
			
			BodyScript.messages.Clear();
		}
		
		//after cost
		if(costFlag && BodyScript.effectTargetID.Count==0){
			costFlag=false;
			BodyScript.effectTargetID.Add(-2);
			BodyScript.effectMotion.Add(6);
		}
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			int deckNum=ManagerScript.getFieldAllNum(0,player);	
			for(int i=0;i<deckNum;i++){
				int x=ManagerScript.getFieldRankID(0,i,player);
				if(x>=0 && checkClass(x,player)){
					BodyScript.Targetable.Add(x+50*player);
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
				burstEffect=true;
			}
		}
		if(burstEffect && BodyScript.effectTargetID.Count==0){
			burstEffect=false;
			BodyScript.Targetable.Clear();
			int target=1-player;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
				if(x>=0 && ManagerScript.getIDConditionInt(x,target)==1){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			int lrigID=ManagerScript.getLrigID(target);
			if(ManagerScript.getIDConditionInt(lrigID,target)==1)
				BodyScript.Targetable.Add(lrigID+50*target);
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(8);
			}
		}
		
		
		field=ManagerScript.getFieldInt(ID,player);	
	}
	
	void untachable(){
		if(ManagerScript.getFieldInt(ID,player)!=3)return;
		for(int i=0;i<50;i++){
			int target=1-player; 
			CardScript sc=ManagerScript.getCardScr(i,target);
			if(sc.effectFlag && sc.Type!=0 &&!checkExist(i,target))effecterList.Add(i+50*target);
		}
		
		for(int i=0;i<effecterList.Count;i++){
			int px=effecterList[i];
			CardScript sc=ManagerScript.getCardScr(px%50,px/50);
			if(!sc.effectFlag){
				effecterList.RemoveAt(i);
				i--;
			}
			else{
				for(int k=0;k<sc.effectTargetID.Count;k++){
					int et=sc.effectTargetID[k];
					if(et>=0 && et/50==player && checkClass(et%50,et/50) && ManagerScript.getFieldInt(et%50,et/50)==3){
						sc.effectTargetID.RemoveAt(k);
						sc.effectMotion.RemoveAt(k);
						for(int j=0;j<sc.TargetID.Count;j++){
							if(sc.TargetID[j]==et){
								sc.TargetID.RemoveAt(j);
								j--;
							}
						}
						k--;
					}
				}
			}
		}
	}
	
/*	void OnGUI() {
//		sss="";
		GUI.Label(new Rect(Screen.width-100,Screen.height/2,100,200),""+sss);
		if(DialogFlag){
			int sw=Screen.width;
			int sh=Screen.height;
			int size_x=sw/6;
			int size_y=size_x/2;
			int buttunSize_x=size_x*4/10;
			int buttunSize_y=buttunSize_x/3;
			Vector3 v=Camera.main.WorldToScreenPoint(transform.position);
			Rect boxRect=new Rect(v.x-size_x/2,sh-v.y-size_y/2,size_x,size_y);
			Rect buttunRect1=new Rect(
				boxRect.x+(size_x-buttunSize_x*2)/4,
				boxRect.y+size_y-buttunSize_y-5,
				buttunSize_x,
				buttunSize_y
				);
			Rect buttunRect2=new Rect(
				boxRect.x+size_x-(size_x-buttunSize_x*2)/4-buttunSize_x,
				buttunRect1.y,
				buttunSize_x,
				buttunSize_y
				);
			GUI.Box(boxRect,"");
			GUI.Label(boxRect,BodyScript.Name+"の効果を発動しますか？");
			if(GUI.Button(buttunRect1,"Yes")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				BodyScript.Cost[0]=0;
				BodyScript.Cost[1]=2;
				BodyScript.Cost[2]=0;
				BodyScript.Cost[3]=0;
				BodyScript.Cost[4]=0;
				BodyScript.Cost[5]=0;
				costFlag=true;
			}
			if(GUI.Button(buttunRect2,"No")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				BodyScript.Targetable.Clear();
			}
		}
	}
	*/
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return (c[0]==4 && c[1]==0);
	}
	bool checkExist(int x,int player){
		for(int i=0;i<effecterList.Count;i++){
			if(x+50*player==effecterList[i])return true;
		}
		return false;
	}
}

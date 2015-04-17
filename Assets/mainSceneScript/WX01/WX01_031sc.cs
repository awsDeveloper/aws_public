using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX01_031sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
//	bool DialogFlag=false;
	List<int> equipList=new List<int>();
	bool ignition=false;
	int handNum=0;
	bool costFlag=false;
	bool cipCost=false;
	
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
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			//cip
			int num=ManagerScript.getFieldAllNum(2,1-player);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(2,i,1-player);
				if(x>=0){
					BodyScript.Targetable.Add(x+50*(1-player));
				}
			}
			int cost=ManagerScript.getEnaColorNum(3,player);
			cost+=ManagerScript.MultiEnaNum(player);
			if(BodyScript.Targetable.Count>0 && cost>=1){
//				ManagerScript.stopFlag=true;
//				DialogFlag=true;
				BodyScript.DialogFlag=true;
			}
			
			num=ManagerScript.getFieldAllNum(2,player);
			//equip
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(2,i,player);
				CardScript sc=ManagerScript.getCardScr(x,player);
				if(sc.Type==3 && sc.CardColor==3 && sc.Cost[0]>0){
					sc.Cost[0]-=1;
					equipList.Add(x+50*player);
				}
			}
		}
		if(ManagerScript.getFieldInt(ID,player)!=3 && field==3 && !BodyScript.BurstFlag){
			while(equipList.Count>0){
				int x=equipList[0];
				CardScript sc=ManagerScript.getCardScr(x%50,x/50);
				sc.Cost[0]+=1;
				equipList.RemoveAt(0);
			}
		}
		if(equipList.Count>0){
			for(int i=0;i<equipList.Count;i++){
				int x=equipList[i];
				if(ManagerScript.getFieldInt(x%50,x/50)!=2){
					CardScript sc=ManagerScript.getCardScr(x%50,x/50);
					sc.Cost[0]+=1;
					equipList.RemoveAt(i);
					i--;
				}				
			}
		}
		if(ManagerScript.getFieldAllNum(2,player)>handNum && ManagerScript.getFieldInt(ID,player)==3){
			int x=ManagerScript.getFieldRankID(2,handNum,player);
			CardScript sc=ManagerScript.getCardScr(x,player);
			if(sc.Type==3 && sc.CardColor==3 && sc.Cost[0]>0){
				sc.Cost[0]-=1;
				equipList.Add(x+50*player);
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				BodyScript.Cost[0]=0;
				BodyScript.Cost[1]=0;
				BodyScript.Cost[2]=0;
				BodyScript.Cost[3]=1;
				BodyScript.Cost[4]=0;
				BodyScript.Cost[5]=0;
				cipCost=true;
			}
			else BodyScript.Targetable.Clear();
			BodyScript.messages.Clear();
		}
		
		if(cipCost && BodyScript.effectTargetID.Count==0){
			cipCost=false;
			BodyScript.effectTargetID.Add(-1);
			BodyScript.effectMotion.Add(19);			
			BodyScript.effectSelecter=1-player;
		}
		
		
		//ignition
		if(BodyScript.Ignition && !ignition){
			int cost=ManagerScript.getEnaColorNum(3,player);
			int mNum=ManagerScript.MultiEnaNum(player);
			
			if(cost+mNum>=2 ){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				BodyScript.Cost[0]=0;
				BodyScript.Cost[1]=0;
				BodyScript.Cost[2]=0;
				BodyScript.Cost[3]=2;
				BodyScript.Cost[4]=0;
				BodyScript.Cost[5]=0;
				costFlag=true;
				BodyScript.Ignition=false;
			}
		}
		if(!BodyScript.effectFlag && costFlag){
			costFlag=false;
			for(int i=0;i<ManagerScript.getFieldAllNum(7,player);i++){
				int x=ManagerScript.getFieldRankID(7,i,player);
				CardScript sc=ManagerScript.getCardScr(x,player);
				if(sc.Type==3 && sc.CardColor==3 ){
					BodyScript.Targetable.Add(x+50*player);
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
				BodyScript.effectSelecter=player;
			}
		}
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			int num=ManagerScript.getFieldAllNum(2,1-player);	
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(2,i,1-player);
				if(x>=0){
					BodyScript.Targetable.Add(x+50*(1-player));
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
				BodyScript.effectSelecter=1-player;
			}
		}
		field=ManagerScript.getFieldInt(ID,player);
		ignition=BodyScript.Ignition;
		handNum=ManagerScript.getFieldAllNum(2,player);
	}
/*	void OnGUI() {
		string sss="";
		for(int i=0;i<equipList.Count;i++){
			sss+=equipList[i]+"\n";
		}
		if(equipList.Count>0)
			GUI.Label(new Rect(Screen.width-100,Screen.height/2,100,100),sss);
		
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
				BodyScript.Cost[1]=0;
				BodyScript.Cost[2]=0;
				BodyScript.Cost[3]=1;
				BodyScript.Cost[4]=0;
				BodyScript.Cost[5]=0;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
			}
			if(GUI.Button(buttunRect2,"No")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				BodyScript.Targetable.Clear();
			}
		}
	}*/
}

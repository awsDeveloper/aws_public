using UnityEngine;
using System.Collections;

public class WX01_033sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
//	bool DialogFlag=false;
	int spellID=-1;
	bool ignition=false;
	bool costFlag_1=false;
	bool costFlag_2=false;

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
//			guiFlag=true;
			int target=player;
			int f=6;
			int num=ManagerScript.getFieldAllNum(f,target);
			int cost=ManagerScript.getEnaColorNum(4,player);
			cost+=ManagerScript.MultiEnaNum(player);
			if(num>=1 && cost>=1){
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
				BodyScript.Cost[1]=0;
				BodyScript.Cost[2]=0;
				BodyScript.Cost[3]=0;
				BodyScript.Cost[4]=1;
				BodyScript.Cost[5]=0;
				costFlag_2=true;
			}
			else BodyScript.Targetable.Clear();
			BodyScript.messages.Clear();
		}		
		
		//after cost_2
		if(!BodyScript.effectFlag && costFlag_2){
			costFlag_2=false;
			int target=player;
			int f=6;
			int num=ManagerScript.getFieldAllNum(f,target);	
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>0){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(16);
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(20);
			}
		}

		//always
		if(ManagerScript.getFieldInt(ID,player)==3){
			int x=ManagerScript.getFieldRankID(8,0,player);
			if(x>=0){
				CardScript sc=ManagerScript.getCardScr(x,player);
				if(sc.Type==3 && sc.CardColor==4 && !sc.BurstFlag)spellID=x;
			}
		}
		else spellID=-1;
		if(spellID>0){
			if(ManagerScript.getFieldInt(spellID,player)==7){
				spellID=-1;
				int deckNum=ManagerScript.getFieldAllNum(0,player);
				if(deckNum>0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(player*50);
					BodyScript.effectMotion.Add(26);
				}
			}
		}
		//ignition
		if(BodyScript.Ignition && !ignition){
			BodyScript.Ignition=false;
			int cost=ManagerScript.getEnaColorNum(4,player);
			cost+=ManagerScript.MultiEnaNum(player);
			if(cost>=2){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				BodyScript.Cost[0]=0;
				BodyScript.Cost[1]=0;
				BodyScript.Cost[2]=0;
				BodyScript.Cost[3]=0;
				BodyScript.Cost[4]=2;
				BodyScript.Cost[5]=0;
				costFlag_1=true;
			}
		}
		if(!BodyScript.effectFlag && costFlag_1){
			costFlag_1=false;
			int tNum=ManagerScript.getFieldAllNum(7,player);
			for(int i=0;i<tNum;i++){
				int x=ManagerScript.getFieldRankID(7,i,player);
				if(x>=0 && ManagerScript.getCardColor(x,player)==4)BodyScript.effectTargetID.Add(x+50*player);
			}
			if(BodyScript.effectTargetID.Count>0){
				BodyScript.effectFlag=true;
				for(int i=0;i<BodyScript.effectTargetID.Count;i++)BodyScript.effectMotion.Add(25);
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(24);
			}
		}
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			int deckNum=ManagerScript.getFieldAllNum(0,player);
			for(int i=0;i<2&&i<deckNum;i++){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(player*50);
				BodyScript.effectMotion.Add(26);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);
		ignition=BodyScript.Ignition;
	}
	
/*	void OnGUI() {
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
				costFlag_2=true;
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				BodyScript.Cost[0]=0;
				BodyScript.Cost[1]=0;
				BodyScript.Cost[2]=0;
				BodyScript.Cost[3]=0;
				BodyScript.Cost[4]=1;
				BodyScript.Cost[5]=0;
			}
			if(GUI.Button(buttunRect2,"No")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				BodyScript.Targetable.Clear();
			}
		}
	}*/
}

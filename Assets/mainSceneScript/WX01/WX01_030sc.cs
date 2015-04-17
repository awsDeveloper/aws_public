using UnityEngine;
using System.Collections;

public class WX01_030sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool chantFlag=true;
	bool DialogFlag=false;
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
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,1-player);
				if(x>0 && ManagerScript.getCardPower(x,1-player)<=12000){
					BodyScript.Targetable.Add(x+50*(1-player));
				}
			}
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(ID+50*player);
			BodyScript.effectMotion.Add(22);
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
				BodyScript.AntiCheck=true;
			}
		}
		
		if(ManagerScript.getFieldInt(ID,player)!=8 && field==8 && !BodyScript.BurstFlag){
			if(!BodyScript.AntiCheck){
				int x=ManagerScript.getLrigID(player);
				CardScript sc=ManagerScript.getCardScr(x,player);
				if(!sc.DoubleCrash){
					sc.DoubleCrash=true;
					chantFlag=true;
				}
			}
			else{
				chantFlag=false;
				BodyScript.AntiCheck=false;
			}
		}
		
		if(chantFlag && ManagerScript.getPhaseInt()==7){
			int x=ManagerScript.getLrigID(player);
			CardScript sc=ManagerScript.getCardScr(x,player);
			sc.DoubleCrash=false;
			chantFlag=false;
		}
		
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			int num=ManagerScript.getFieldAllNum(5,player);
			int num2=ManagerScript.getFieldAllNum(5,1-player);
			if(num>0 && num2>0){
				BodyScript.DialogFlag=true;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				int num=ManagerScript.getFieldAllNum(5,player);
				int num2=ManagerScript.getFieldAllNum(5,1-player);
				BodyScript.effectTargetID.Add(ManagerScript.getFieldRankID(5,num-1,player)+player*50);
				BodyScript.effectMotion.Add(7);
				BodyScript.effectTargetID.Add(ManagerScript.getFieldRankID(5,num2-1,1-player)+(1-player)*50);
				BodyScript.effectMotion.Add(5);
			}
			else BodyScript.Targetable.Clear();
			
			BodyScript.messages.Clear();
		}

		field=ManagerScript.getFieldInt(ID,player);
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
				int num=ManagerScript.getFieldAllNum(5,player);
				int num2=ManagerScript.getFieldAllNum(5,1-player);
				BodyScript.effectTargetID.Add(ManagerScript.getFieldRankID(5,num-1,player)+player*50);
				BodyScript.effectMotion.Add(7);
				BodyScript.effectTargetID.Add(ManagerScript.getFieldRankID(5,num2-1,1-player)+(1-player)*50);
				BodyScript.effectMotion.Add(5);
			}
			if(GUI.Button(buttunRect2,"No")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
			}
		}
	}*/
}

using UnityEngine;
using System.Collections;

public class WX01_044sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool chantFlag=false;
	
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
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,1-player);
				if(x>0 && ManagerScript.getCardLevel(x,1-player)<=3){
					BodyScript.Targetable.Add(x+50*(1-player));
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(27);
				BodyScript.effectSelecter=player;
			}
		}
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			int num=ManagerScript.getFieldAllNum(2,player);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(2,i,player);
				if(x>=0)BodyScript.Targetable.Add(x+50*player);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.DialogFlag=true;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				chantFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
				BodyScript.effectSelecter=player;
			}
			else BodyScript.Targetable.Clear();
			BodyScript.messages.Clear();
		}
		
		if(BodyScript.effectTargetID.Count==0 && chantFlag){
			chantFlag=false;
			BodyScript.Targetable.Clear();
			int num=ManagerScript.getFieldAllNum(2,1-player);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(2,i,1-player);
				if(x>=0)BodyScript.Targetable.Add(x+50*(1-player));
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
				BodyScript.effectSelecter=1-player;
			}
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
				chantFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
			}
			if(GUI.Button(buttunRect2,"No")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
			}
		}
	}*/
}

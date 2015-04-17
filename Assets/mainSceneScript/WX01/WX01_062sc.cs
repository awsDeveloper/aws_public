using UnityEngine;
using System.Collections;

public class WX01_062sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool showFlag=false;
	bool DialogFlag=false;
	int count=5;
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
			BodyScript.effectFlag=true;
			showFlag=true;
			BodyScript.AntiCheck=true;
			BodyScript.effectTargetID.Add(ID+50*player);
			BodyScript.effectMotion.Add(22);			
			int deckNum=ManagerScript.getFieldAllNum(0,player);
			for(int i=0;i<5&&i<deckNum;i++){
				int moveID=ManagerScript.getFieldRankID(0,deckNum-1-i,player);
				BodyScript.effectTargetID.Add(moveID + 50*player);
				BodyScript.effectMotion.Add(14);
			}
		}
		if(showFlag && BodyScript.effectTargetID.Count==0){
			showFlag=false;
			if(!BodyScript.AntiCheck){
				ManagerScript.stopFlag=true;
				DialogFlag=true;
			}
			else BodyScript.AntiCheck=false;
		}
		
		field=ManagerScript.getFieldInt(ID,player);
	}
	void OnGUI() {
		if(DialogFlag){
			int deckNum=ManagerScript.getFieldAllNum(0,player);
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
				boxRect.y+size_y/2-buttunSize_y/2,
				buttunSize_x,
				buttunSize_y
				);
			Rect buttunRect2=new Rect(
				boxRect.x+size_x-(size_x-buttunSize_x*2)/4-buttunSize_x,
				buttunRect1.y,
				buttunSize_x,
				buttunSize_y
				);
			Rect buttunRect3=new Rect(
				boxRect.x+size_x/2-buttunSize_x/2,
				boxRect.y+size_y-buttunSize_y-5,
				buttunSize_x,
				buttunSize_y
				);
			GUI.Box(boxRect,"");
			GUI.Label(boxRect,"墓地に送る数 -> "+count);
			if(GUI.Button(buttunRect1,"+")){
				count++;
				if(count==6 ||count>deckNum)count=0;
			}
			if(GUI.Button(buttunRect2,"-")){
				count--;
				if(count==-1)count=5;
				if(count>deckNum)count=deckNum;
			}
			if(GUI.Button(buttunRect3,"ok")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				if(count>0){
					for(int i=0; i<count; i++){
						int x=ManagerScript.getFieldRankID(0,deckNum-1-i,player);
						BodyScript.effectTargetID.Add(x+player*50);
						BodyScript.effectMotion.Add(7);
					}
					BodyScript.effectFlag=true;
				}
			}
		}
	}
}

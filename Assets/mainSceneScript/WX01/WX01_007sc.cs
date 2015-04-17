using UnityEngine;
using System.Collections;

public class WX01_007sc : MonoBehaviour {
	
	//WX03_011 参照
	
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
//	bool DialogFlag=false;
	bool costFlag=false;

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
		if(ManagerScript.getFieldInt(ID,player)==4 && field!=4 && !BodyScript.BurstFlag){
			int deckNum=ManagerScript.getFieldAllNum(0,player);	
			for(int i=0;i<deckNum;i++){
				int x=ManagerScript.getFieldRankID(0,i,player);
				if(x>0 && ManagerScript.getCardType(x,player)==2 
					&& ManagerScript.getCardLevel(x,player)<=2 && ManagerScript.getCardColor(x,player)==1){
					BodyScript.Targetable.Add(x+50*player);
				}
			}
			if(BodyScript.Targetable.Count>0){
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
				BodyScript.Cost[1]=1;
				BodyScript.Cost[2]=0;
				BodyScript.Cost[3]=0;
				BodyScript.Cost[4]=0;
				BodyScript.Cost[5]=0;
				costFlag=true;
			}
			else BodyScript.Targetable.Clear();
			BodyScript.messages.Clear();
		}
		
		if(costFlag && BodyScript.effectTargetID.Count==0){
			costFlag=false;
			BodyScript.effectTargetID.Add(-2);
			BodyScript.effectMotion.Add(16);
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
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				BodyScript.Cost[0]=0;
				BodyScript.Cost[1]=1;
				BodyScript.Cost[2]=0;
				BodyScript.Cost[3]=0;
				BodyScript.Cost[4]=0;
				BodyScript.Cost[5]=0;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
			if(GUI.Button(buttunRect2,"No")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				BodyScript.Targetable.Clear();
			}
		}
	}*/
}

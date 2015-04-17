using UnityEngine;
using System.Collections;

public class WD05_008sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool DialogFlag=false;
	int count=1;

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
//			ManagerScript.stopFlag=true;
//			DialogFlag=true;
			count=ManagerScript.getFieldAllNum(7,player);
			if(count>2)count=3;
			
			if(count>0){
				BodyScript.DialogFlag=true;
				BodyScript.DialogCountMax=count;
				BodyScript.DialogNum=1;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			count=int.Parse(BodyScript.messages[0]);
			BodyScript.messages.Clear();

			if(count>0){
				int trashNum=ManagerScript.getFieldAllNum(7,player);
				for(int i=0;i<trashNum;i++){
					int x=ManagerScript.getFieldRankID(7,i,player);
					if(x>=0 && ManagerScript.getCardType(x,player)==2){
						BodyScript.Targetable.Add(x+50*player);
					}
				}
				if(BodyScript.Targetable.Count>0){
					for(int i=0; i<BodyScript.Targetable.Count && i<count; i++){
						BodyScript.effectTargetID.Add(-2);
						BodyScript.effectMotion.Add(16);
					}
					BodyScript.effectFlag=true;
				}
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
			GUI.Label(boxRect,"対象の数 -> "+count);
			if(GUI.Button(buttunRect1,"+")){
				count++;
				if(count==4)count=0;
			}
			if(GUI.Button(buttunRect2,"-")){
				count--;
				if(count==-1)count=3;
			}
			if(GUI.Button(buttunRect3,"ok")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				if(count>0){
					int trashNum=ManagerScript.getFieldAllNum(7,player);
					for(int i=0;i<trashNum;i++){
						int x=ManagerScript.getFieldRankID(7,i,player);
						if(x>=0 && ManagerScript.getCardType(x,player)==2){
							BodyScript.Targetable.Add(x+50*player);
						}
					}
					if(BodyScript.Targetable.Count>0){
						for(int i=0; i<BodyScript.Targetable.Count && i<count; i++){
							BodyScript.effectTargetID.Add(-2);
							BodyScript.effectMotion.Add(16);
						}
						BodyScript.effectFlag=true;
					}
				}
			}
		}
	}*/
}

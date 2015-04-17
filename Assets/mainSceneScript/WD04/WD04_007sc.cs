using UnityEngine;
using System.Collections;

public class WD04_007sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
//	bool DialogFlag=false;
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
			count=ManagerScript.getFieldAllNum(6,player);
			if(count>2)count=2;

			BodyScript.DialogFlag=true;
			BodyScript.DialogNum=1;
			BodyScript.DialogCountMax=count;
		}
	
		//receive
		if(BodyScript.messages.Count>0){
			count=int.Parse(BodyScript.messages[0]);
			BodyScript.messages.Clear();
			
			if(count>0){
				int num=ManagerScript.getFieldAllNum(6,player);
				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(6,i,player);
					if(x>=0){
						BodyScript.Targetable.Add(x+50*player);
					}
				}
				if(BodyScript.Targetable.Count>0){
					for(int i=0; i<BodyScript.Targetable.Count && i<count; i++){
						BodyScript.effectTargetID.Add(-1);
						BodyScript.effectMotion.Add(16);
					}
					BodyScript.effectTargetID.Add(50*player);
					BodyScript.effectMotion.Add(20);
					BodyScript.effectFlag=true;
				}
			}
		}	
		
		//UpDate
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
				if(count==3)count=0;
			}
			if(GUI.Button(buttunRect2,"-")){
				count--;
				if(count==-1)count=2;
			}
			if(GUI.Button(buttunRect3,"ok")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				if(count>0){
					int num=ManagerScript.getFieldAllNum(6,player);
					for(int i=0;i<num;i++){
						int x=ManagerScript.getFieldRankID(6,i,player);
						if(x>=0){
							BodyScript.Targetable.Add(x+50*player);
						}
					}
					if(BodyScript.Targetable.Count>0){
						for(int i=0; i<BodyScript.Targetable.Count && i<count; i++){
							BodyScript.effectTargetID.Add(-1);
							BodyScript.effectMotion.Add(16);
						}
						BodyScript.effectTargetID.Add(50*player);
						BodyScript.effectMotion.Add(20);
						BodyScript.effectFlag=true;
					}
				}
			}
		}
	}*/
}

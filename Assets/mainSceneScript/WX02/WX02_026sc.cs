using UnityEngine;
using System.Collections;

public class WX02_026sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
//	bool DialogFlag=false;
	int count=0;
	bool chantFlag=false;
	bool burstEffect=false;
	int notCipID=-1;
	bool stopFlag=false;
	int stopCount=0;
	
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
		
		
		//chant
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int target=player;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
				if(checkClass(x,target))BodyScript.Targetable.Add(x+50*target);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(7);
				chantFlag=true;
				BodyScript.TargetIDEnable=true;
			}
		}
		
		//after cost
		if(BodyScript.effectTargetID.Count==0 && chantFlag){
			chantFlag=false;
			if(BodyScript.TargetID.Count==1){
				BodyScript.TargetID.Clear();
				BodyScript.Targetable.Clear();
				int target=player;
				int f=0;
				int num=ManagerScript.getFieldAllNum(f,target);
				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(f,i,target);
					if(checkClass(x,target))BodyScript.Targetable.Add(x+50*target);
				}
				if(BodyScript.Targetable.Count>0){
//					ManagerScript.stopFlag=true;
//					DialogFlag=true;
					count=BodyScript.Targetable.Count;
					if(count>2)count=2;
					BodyScript.DialogFlag=true;
					BodyScript.DialogNum=1;
					BodyScript.DialogCountMax=count;
				}
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			count=int.Parse(BodyScript.messages[0]);
			BodyScript.messages.Clear();
			
			if(count>0){				
				for(int i=0; i<BodyScript.Targetable.Count && i<count; i++){
					BodyScript.effectTargetID.Add(-2);
					BodyScript.effectMotion.Add(31);
				}
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(24);
			}
			else BodyScript.Targetable.Clear();
		}		
		
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag && ManagerScript.getFieldAllNum(3,player)<3){
			int target=player;
			int f=0;
			int num=ManagerScript.getFieldAllNum(f,target);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(checkClass(x,target))
					BodyScript.Targetable.Add(x+50*target);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(6);
				burstEffect=true;
			}
		}
		
		//look cip
		if(burstEffect && BodyScript.effectTargetID.Count==1 &&BodyScript.effectTargetID[0]>=0){
			burstEffect=false;
			notCipID=BodyScript.effectTargetID[0];
		}
		//after 1 frame check
		if(stopFlag){
			stopCount--;
			CardScript sc=ManagerScript.getCardScr(notCipID%50,notCipID/50);
			sc.effectFlag=false;
			sc.DialogFlag=false;
			sc.effectTargetID.Clear();
			sc.effectMotion.Clear();
			if(ManagerScript.stopFlag){
				stopCount=5;
			}
		}
		if(stopCount<0 && stopFlag){
			stopFlag=false;
			ManagerScript.cipCheck=false;
			notCipID=-1;
		}
		//stopFlag up
		if(notCipID>=0 && !BodyScript.effectFlag && !stopFlag){
			ManagerScript.cipCheck=true;
			stopFlag=true;
			stopCount=5;
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
				if(count>2 || count>BodyScript.Targetable.Count)count=0;
			}
			if(GUI.Button(buttunRect2,"-")){
				count--;
				if(count==-1)count=BodyScript.Targetable.Count;
				if(count>2)count=2;
			}
			if(GUI.Button(buttunRect3,"ok")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				if(count>0){
					BodyScript.effectFlag=true;					
					for(int i=0; i<BodyScript.Targetable.Count && i<count; i++){
						BodyScript.effectTargetID.Add(-2);
						BodyScript.effectMotion.Add(31);
					}
					BodyScript.effectTargetID.Add(50*player);
					BodyScript.effectMotion.Add(24);
				}
				else BodyScript.Targetable.Clear();
			}
		}
	}*/
	
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return (c[0]==4 && c[1]==0);
	}
}

using UnityEngine;
using System.Collections;

public class WX02_035sc : MonoBehaviour {
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
		//cip
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			int target=player;
			int f=2;
			int num=ManagerScript.getFieldAllNum(f,target);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(checkClass(x,target)){
					BodyScript.Targetable.Add(x+50*target);
				}		
			}
			bool flag=false;
			f=3;
			num=3;//ManagerScript.getFieldAllNum(f,target);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardType(x,target)==2)flag=true;
			}
			if(BodyScript.Targetable.Count>0 && flag){
//				ManagerScript.stopFlag=true;
//				DialogFlag=true;
				BodyScript.DialogFlag=true;
			}
			else BodyScript.Targetable.Clear();
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
				costFlag=true;
			}
			else BodyScript.Targetable.Clear();
			
			BodyScript.messages.Clear();
		}
		
		//after cost
		if(costFlag && BodyScript.effectTargetID.Count==0){
			costFlag=false;
			BodyScript.Targetable.Clear();
			int target=1-player;
			int f=3;
			int num=3;//ManagerScript.getFieldAllNum(f,target);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardType(x,target)==2)BodyScript.Targetable.Add(x+50*target);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(27);
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(2);
			}			
		}
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(26);
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
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
				costFlag=true;
			}
			if(GUI.Button(buttunRect2,"No")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				BodyScript.Targetable.Clear();
			}
		}
	}*/
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return c[0]==3 && c[1]==0;
	}
}

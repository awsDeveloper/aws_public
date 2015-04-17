using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX02_024sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
//	bool DialogFlag=false;
	bool costFlag=false;
	
//	const int selectMax=1;
	List<int> checkID=new List<int>();
//	bool[] checkBox=new bool[2];
	bool effectFlag=false;
	
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=2000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();

	
		//dialog 2
		BodyScript.checkStr.Add("バニッシュ");
		BodyScript.checkStr.Add("エナチャージ");
		
		for(int i=0;i<BodyScript.checkStr.Count;i++){
			BodyScript.checkBox.Add(false);		
		}
	}
	
	// Update is called once per frame
	void Update () {
		//cip
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			int target=1-player;
			int f=3;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardPower(x,target)>=15000)BodyScript.Targetable.Add(x+50*target);
			}
			if(BodyScript.Targetable.Count>0){
//				ManagerScript.stopFlag=true;
//				DialogFlag=true;
				BodyScript.DialogFlag=true;
				BodyScript.DialogNum=0;
			}
		}
		//cip after cost
		if(costFlag && BodyScript.effectTargetID.Count==0){
			costFlag=false;
			BodyScript.effectTargetID.Add(-1);
			BodyScript.effectMotion.Add(5);
		}
		
		//ignition
		if(BodyScript.Ignition){
			BodyScript.Ignition=false;
			int target=player;
			int f=3;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(checkClass(x,target) && ManagerScript.getIDConditionInt(x,target)==1)
					BodyScript.Targetable.Add(x+target*50);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(8);
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(26);
			}			
		}
		
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
//			ManagerScript.stopFlag=true;
//			DialogFlag=true;
			effectFlag=true;
			BodyScript.DialogFlag=true;
			BodyScript.DialogNum=2;
			BodyScript.DialogCountMax=1;
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(effectFlag){
				effectFlag=false;
				
				if(BodyScript.messages[0].Contains("Yes")){
					for(int i=0;i<BodyScript.checkBox.Count;i++){
						if(BodyScript.checkBox[i])checkID.Add(i);
					}
					effect_1();
					effect_2();
				}
				else destory();				
			}
			else{
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
					costFlag=true;
				}
				else BodyScript.Targetable.Clear();
			}
			
			BodyScript.messages.Clear();
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
			if(effectFlag){
				string[] checkStr=new string[checkBox.Length];
				checkStr[0]="バニッシュ";
				checkStr[1]="エナチャージ";
				for(int i=0;i<checkBox.Length;i++){
					float height=(size_y-buttunSize_y-5)/checkBox.Length;
					Rect rect=new Rect(buttunRect1.x,boxRect.y+height*i,size_x,height);
					bool flag=checkBox[i];
					checkBox[i]=GUI.Toggle(rect,checkBox[i],checkStr[i]);
					if(checkBox[i] && !flag){
						checkID.Add(i);
						if(checkID.Count>selectMax)checkBox[checkID[0]]=false;
					}
					if(!checkBox[i])checkID.Remove(i);
				}
				
				if(GUI.Button(buttunRect1,"OK")){
					ManagerScript.stopFlag=false;
					DialogFlag=false;
					effectFlag=false;
					effect_1();
					effect_2();
				}
				if(GUI.Button(buttunRect2,"Cancel")){
					ManagerScript.stopFlag=false;
					DialogFlag=false;
					effectFlag=false;
					destory();
				}
			}
			else{
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
					BodyScript.Cost[3]=0;
					BodyScript.Cost[4]=1;
					BodyScript.Cost[5]=0;
					costFlag=true;
				}
				if(GUI.Button(buttunRect2,"No")){
					ManagerScript.stopFlag=false;
					DialogFlag=false;
					BodyScript.Targetable.Clear();
				}
			}
		}
	}*/
	
	void destory(){
		while(checkID.Count>0){
			BodyScript.checkBox[checkID[0]]=false;
			checkID.RemoveAt(0);
		}
	}
	void effect_1(){
		if(BodyScript.checkBox[0]){
			int target=1-player;
			int f=3;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardPower(x,target)>=10000)BodyScript.Targetable.Add(x+50*target);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}
		}
	}
	void effect_2(){
		if(BodyScript.checkBox[1]){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(26);
		}
	}
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return (c[0]==2 && c[1]==2);
	}
}

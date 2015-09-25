using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PR_040 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	const int selectMax=2;
	List<int> checkID=new List<int>();
	bool chantFlag=false;
	
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		string[] checkStr=new string[4];
		checkStr[0]="ルリグストップ";
		checkStr[1]="FREEZE";
		checkStr[2]="バウンス";
		checkStr[3]="ドローツー";
		for(int i=0;i<4;i++){
			BodyScript.checkStr.Add(checkStr[i]);
		}
		
		for(int i=0;i<BodyScript.checkStr.Count;i++){
			BodyScript.checkBox.Add(false);		
		}

        BodyScript.attackArts = true;		
	}
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			if(checkID.Count==0){
				BodyScript.DialogFlag=true;
				BodyScript.DialogCountMax=2;
				BodyScript.DialogNum=2;
			}
			else{
				effect_1();
				effect_2();
				effect_3();
				effect_4();
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				for(int i=0;i<BodyScript.checkBox.Count;i++){
					if(BodyScript.checkBox[i])checkID.Add(i);
				}
				chantFlag=true;
				effect_1();
				effect_2();
				effect_3();
				effect_4();
			}
			else {
				destory();
			}
			
			BodyScript.messages.Clear();
		}
		
		//down chant flag
		if(BodyScript.effectTargetID.Count==0 && chantFlag){
			chantFlag=false;
			destory();
		}
		
		field=ManagerScript.getFieldInt(ID,player);
	}
/*	void OnGUI() {
		string sss="";
		for(int i=0;i<checkID.Count;i++)sss+=checkID[i]+" ";
		GUI.Label(new Rect(Screen.width-100,Screen.height/2,100,100),sss);
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
			
			for(int i=0;i<BodyScript.checkBox.Count;i++){
				float height=(size_y-buttunSize_y-5)/BodyScript.checkBox.Count;
				Rect rect=new Rect(buttunRect1.x,boxRect.y+height*i,size_x,height);
				bool flag=BodyScript.checkBox[i];
				BodyScript.checkBox[i]=GUI.Toggle(rect,BodyScript.checkBox[i],BodyScript.checkStr[i]);
				if(BodyScript.checkBox[i] && !flag){
					checkID.Add(i);
					if(checkID.Count>selectMax)BodyScript.checkBox[checkID[0]]=false;
				}
				if(!BodyScript.checkBox[i])checkID.Remove(i);
			}
			
			if(GUI.Button(buttunRect1,"OK")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				chantFlag=true;
				effect_1();
				effect_2();
				effect_3();
				effect_4();
			}
			if(GUI.Button(buttunRect2,"Cancel")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				destory();
			}
		}
	}*/
	
	void effect_1(){
		if(BodyScript.checkBox[0]){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(ManagerScript.getLrigID(1-player)+50*(1-player));
			BodyScript.effectMotion.Add(18);
		}
	}
	void effect_2(){
		if(BodyScript.checkBox[1]){
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,1-player);
				if(x>=0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(x+50*(1-player));
					BodyScript.effectMotion.Add(27);
				}
			}
		}
	}
	void effect_3(){
		if(BodyScript.checkBox[2]){
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,1-player);
				if(x>=0)BodyScript.Targetable.Add(x+50*(1-player));
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(16);
			}
		}
	}
	void effect_4(){
		if(BodyScript.checkBox[3]){
			for(int i=0;i<2 && i<ManagerScript.getFieldAllNum(0,player);i++){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(2);
			}
		}
	}
	
	void destory(){
		while(checkID.Count>0){
			BodyScript.checkBox[checkID[0]]=false;
			checkID.RemoveAt(0);
		}
	}
}

using UnityEngine;
using System.Collections;

public class WX02_001 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool DialogFlag=false;
	bool costFlag_1=false;
	bool costFlag_2=false;
	bool cipFlag=false;
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=5000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==4 &&field!=4 && enaCheclk(new int[6]{0,1,0,0,0,0})){
			int target=player;
			int f=0;
			int num=ManagerScript.getFieldAllNum(f,target);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardType(x, target)==2 )BodyScript.Targetable.Add(x+50*target);
			}
			if(BodyScript.Targetable.Count>0){
				ManagerScript.stopFlag=true;
				DialogFlag=true;
				cipFlag=true;
			}
		}
		
		if(BodyScript.effectTargetID.Count==0 && cipFlag  && !DialogFlag){
			cipFlag = false;
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
		}
		
		if(ID==ManagerScript.getLrigID(player)){
			if(BodyScript.Ignition){				
				BodyScript.Ignition=false;
				
				bool[] flag=new bool[2];
				int target=1-player;
				int[] count=new int[2]{0,0};
				
				//ignition check 1
				for(int i=0;i<3;i++){
					int x=ManagerScript.getFieldRankID(3,i,target);
					if(x>=0 && ManagerScript.getCardPower(x,target)<=7000)count[0]++;
				}
				if(enaCheclk(new int[6]{0,1,1,0,0,0}) && count[0]>=1)flag[0]=true;
				
				//ignition check 2
				for(int i=0;i<3;i++){
					int x=ManagerScript.getFieldRankID(3,i,target);
					if(x>=0 && ManagerScript.getCardPower(x,target)>=10000)count[1]++;
				}
				if(count[0]>=1 && enaCheclk(new int[6]{0,1,1,0,0,0}))flag[0]=true;
				if(count[1]>=1 && enaCheclk(new int[6]{1,1,0,0,1,0}))flag[1]=true;
				
				//select root
				if(flag[0] && flag[1]){
					ManagerScript.stopFlag=true;
					DialogFlag=true;
				}
				else if(flag[0]){
					effect_1();
				}
				else if(flag[1]){
					effect_2();
				}
				
			}
		}
		//ignition 1 after cost
		if(BodyScript.effectTargetID.Count==0 && costFlag_1){
			costFlag_1=false;
			int target=1-player;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
					if(x>=0 && ManagerScript.getCardPower(x,target)<=7000)BodyScript.Targetable.Add(x+50*target);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}
		}
		
		//ignition 2 after cost
		if(BodyScript.effectTargetID.Count==0 && costFlag_2){
			costFlag_2=false;
			int target=1-player;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
					if(x>=0 && ManagerScript.getCardPower(x,target)>=10000)BodyScript.Targetable.Add(x+50*target);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	void effect_1(){
		costFlag_1=true;
		BodyScript.effectFlag=true;
		BodyScript.effectTargetID.Add(ID+player*50);
		BodyScript.effectMotion.Add(17);
		BodyScript.Cost[0]=0;
		BodyScript.Cost[1]=1;
		BodyScript.Cost[2]=1;
		BodyScript.Cost[3]=0;
		BodyScript.Cost[4]=0;
		BodyScript.Cost[5]=0;
	}
	void effect_2(){
		costFlag_2=true;
		BodyScript.effectFlag=true;
		BodyScript.effectTargetID.Add(ID+player*50);
		BodyScript.effectMotion.Add(17);
		BodyScript.Cost[0]=1;
		BodyScript.Cost[1]=1;
		BodyScript.Cost[2]=0;
		BodyScript.Cost[3]=0;
		BodyScript.Cost[4]=1;
		BodyScript.Cost[5]=0;
	}
	
	
	void OnGUI() {
//		GUI.Label(new Rect(Screen.width-100,Screen.height/2,100,100),"costCount="+costCount);
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
			if(cipFlag){
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
				}
				if(GUI.Button(buttunRect2,"No")){
					ManagerScript.stopFlag=false;
					DialogFlag=false;
					cipFlag=false;
					BodyScript.Targetable.Clear();
				}
			}
			else{
				GUI.Label(boxRect,"効果を選択してください");
				if(GUI.Button(buttunRect1,"7000 以下")){
					ManagerScript.stopFlag=false;
					DialogFlag=false;
					effect_1();
				}
				if(GUI.Button(buttunRect2,"10000 以上")){
					ManagerScript.stopFlag=false;
					DialogFlag=false;
					effect_2();
				}
			}
		}
	}
	bool enaCheclk(int[] cost){
		if(cost.Length!=6)return false;
		int sum_cost=0;
		//0 check
		for(int i=0;i<6;i++){
			sum_cost+=cost[i];
		}
		if(sum_cost>ManagerScript.getFieldAllNum(6,player))return false;
		int multi=ManagerScript.MultiEnaNum(player);
		//1~5 check
		for(int i=1;i<6;i++){
			int num=ManagerScript.getEnaColorNum(i,player);
			if(num<cost[i]){
				multi-=cost[i]-num;
			}
		}
		return multi>=0;
	}
}

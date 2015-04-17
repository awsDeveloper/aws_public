using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX02_003sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	List<int> equipList=new List<int>();
	int LifeClothNum=0;
	int lookingID=-1;
	bool DialogFlag=false;
	
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=3000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		//cip
		if(ManagerScript.getFieldInt(ID,player)==4 && field!=4 && !BodyScript.BurstFlag){
			int target=player;
			//equip
			int num=ManagerScript.getFieldAllNum(5,target);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(5,i,target);
				equip(x,target);				
			}
			
		}
		//pig
		if(ID!=ManagerScript.getLrigID(player)){
			while(equipList.Count>0){
				dequip(0);
			}
		}
		//equip check
		if(ManagerScript.getFieldAllNum(5,player)>LifeClothNum && ID==ManagerScript.getLrigID(player)){
			int x=ManagerScript.getFieldRankID(5,LifeClothNum,player);
			equip(x,player);
		}
		for(int i=0;i<equipList.Count;i++){
			int x=equipList[i];
			if(ManagerScript.getFieldInt(x%50,x/50)!=5){
				if(ManagerScript.getFieldInt(x%50,x/50)==8){
					lookingID=x;
				}
				dequip(i);
				i--;
			}
		}
		if(lookingID>=0 && ManagerScript.getFieldInt(lookingID%50,lookingID/50)==6){
			lookingID=-1;
			DialogFlag=true;
			ManagerScript.stopFlag=true;			
		}
		
		field=ManagerScript.getFieldInt(ID,player);
		LifeClothNum=ManagerScript.getFieldAllNum(5,player);
	}
	void OnGUI() {
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
				BodyScript.effectTargetID.Add(player*50);
				BodyScript.effectMotion.Add(2);
				}
			if(GUI.Button(buttunRect2,"No")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				BodyScript.Targetable.Clear();
			}
		}
	}
	
	void equip(int x,int eplayer){
		if(x<0)return;
		equipList.Add(x+50*eplayer);
	}
	void dequip(int index){
		if(index>=equipList.Count)return;
		equipList.RemoveAt(index);
	}
}

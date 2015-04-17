using UnityEngine;
using System.Collections;

public class WX02_073 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
//	bool DialogFlag=false;
	
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
		if(BodyScript.DialogFlag && (ManagerScript.getFieldInt(ID,player)!=7 || ManagerScript.getFieldAllNum(3,player)>=3) ){
			BodyScript.DialogFlag=false;
		}
		
		if(ManagerScript.getFieldInt(ID,player)==7 && field==0){
			if(ManagerScript.getFieldAllNum(3,player)<3){
//				ManagerScript.stopFlag=true;
//				DialogFlag=true;
				BodyScript.DialogFlag=true;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(6);
			}
			else BodyScript.Targetable.Clear();
			
			BodyScript.messages.Clear();
		}
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(2);		
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
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(6);
			}
			if(GUI.Button(buttunRect2,"No")){
				ManagerScript.stopFlag=false;
				DialogFlag=false;
				BodyScript.Targetable.Clear();
			}
		}
	}*/
}

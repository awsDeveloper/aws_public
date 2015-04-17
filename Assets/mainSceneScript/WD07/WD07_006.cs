using UnityEngine;
using System.Collections;

public class WD07_006 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool[] levelArray=new bool[4];
	int count=0;
	
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
			reset();
			effect();
		}		
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				effect();
			}
			else shuffule();
			
			BodyScript.messages.Clear();
		}
		
		//入力
		if(BodyScript.effectMotion.Count>0 ){
			int c=BodyScript.effectMotion.Count;
			if(BodyScript.effectMotion[c-1]==7 && BodyScript.effectTargetID[c-1]!=-2 && !BodyScript.DialogFlag){
				count++;
				
				int id=BodyScript.effectTargetID[c-1];
				levelArray[ManagerScript.getCardLevel(id%50,id/50)-1]=true;
				
				if(count<3){
					BodyScript.DialogFlag=true;
				}
				else{
					shuffule();
				}
			}
		}
		
		field=ManagerScript.getFieldInt(ID,player);	
	}
	
	bool checkArray(int level){
		if(level<0)return false;
		return !levelArray[level-1];
	}
	
	void effect(){
		BodyScript.Targetable.Clear();
		
		int f=0;
		int target=player;
		int num=ManagerScript.getFieldAllNum(f,target);
		if(f==3)num=3;
		
		for(int i=0;i<num;i++){
			int x=ManagerScript.getFieldRankID(f,i,target);
			if(x>=0 && checkArray(ManagerScript.getCardLevel(x,target))){
				BodyScript.Targetable.Add(x+50*target);
			}
		}
		
		if(BodyScript.Targetable.Count>0){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(-2);
			BodyScript.effectMotion.Add(7);
		}
		else shuffule();
	}
	
	void shuffule(){
		BodyScript.effectFlag=true;
		BodyScript.effectTargetID.Add(player*50);
		BodyScript.effectMotion.Add(24);
	}
	
	void reset(){
		count=0;
		for(int i=0;i<levelArray.Length;i++)levelArray[i]=false;
	}
}

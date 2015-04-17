using UnityEngine;
using System.Collections;

public class WX03_016 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
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
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			BodyScript.Cost[0]=0;
			BodyScript.Cost[1]=1;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=0;
			
			if(ManagerScript.checkCost(ID,player) && ManagerScript.getFieldAllNum(3,1-player)>0){
				 BodyScript.DialogFlag=true;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				int target=player;
				int f=1;
				int num=ManagerScript.getFieldAllNum(f,target);
				
				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(f,i,target);
					if(ManagerScript.getCardType(x,target)==1 && ManagerScript.getCardColor(x,target)==1){
						BodyScript.Targetable.Add(x+50*target);
					}
				}
				
				if(BodyScript.Targetable.Count>0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(-2);
					BodyScript.effectMotion.Add(36);
					BodyScript.effectTargetID.Add(ID+player*50);
					BodyScript.effectMotion.Add(17);
					costFlag=true;
				}
			}
			else BodyScript.Targetable.Clear();
			
			BodyScript.messages.Clear();
		}
		
		if(costFlag && BodyScript.effectTargetID.Count==0){
			costFlag=false;
			BodyScript.Targetable.Clear();
			
			int target=1-player;
			int f=3;
			int num=ManagerScript.getFieldAllNum(f,target);
			if(f==3)num=3;
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(16);
			}
		}
		
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

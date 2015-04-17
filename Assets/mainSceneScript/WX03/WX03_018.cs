using UnityEngine;
using System.Collections;

public class WX03_018 : MonoBehaviour {
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
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=1;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=0;
			
			if(ManagerScript.checkCost(ID,player)){
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
					if(ManagerScript.getCardType(x,target)==1 && ManagerScript.getCardColor(x,target)==BodyScript.CardColor){
						BodyScript.Targetable.Add(x+50*target);
					}
				}
				
				if(BodyScript.Targetable.Count>0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(-2);
					BodyScript.effectMotion.Add(36);
					BodyScript.effectTargetID.Add(ID+50*player);
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
			
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(22);
			for(int i=0;i<2 && i<ManagerScript.getFieldAllNum(0,player);i++){
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(2);
			}
			
		}
		
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

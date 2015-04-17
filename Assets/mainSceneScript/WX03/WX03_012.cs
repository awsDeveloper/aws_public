using UnityEngine;
using System.Collections;

public class WX03_012 : MonoBehaviour {
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
		if(ManagerScript.getFieldInt(ID,player)==4 && field!=4){
			BodyScript.Cost[0]=0;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=1;
			if(ManagerScript.checkCost(ID,player)){
				BodyScript.DialogFlag=true;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				costFlag=true;
			}
			else BodyScript.Targetable.Clear();
			BodyScript.messages.Clear();
		}
		
		if(costFlag && BodyScript.effectTargetID.Count==0){
			costFlag=false;
			
			int f=0;
			int target=player;
			int num=ManagerScript.getFieldAllNum(f,target);
			if(f==3)num=3;
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(7);
				BodyScript.effectTargetID.Add(player*50);
				BodyScript.effectMotion.Add(24);
			}
		}
		
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

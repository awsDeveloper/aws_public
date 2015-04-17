using UnityEngine;
using System.Collections;

public class WD06_018 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool openFlag=false;
	int openID=-1;
	
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
			int f=5;
			int target=1-player;		
			int num=ManagerScript.getNumForCard(f,target);
			if(num>0){
				openFlag=true;
				openID=ManagerScript.getFieldRankID(f,num-1,target)+50*target;
				
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(50*target);
				BodyScript.effectMotion.Add(39);
			}
		}
		
		//afetr open
		if(openFlag && BodyScript.effectTargetID.Count==0){
			openFlag=false;
			BodyScript.DialogFlag=true;
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(22);		
			
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectTargetID.Add(openID);
				BodyScript.effectMotion.Add(7);	
				BodyScript.effectTargetID.Add((1-player)*50);
				BodyScript.effectMotion.Add(41);	
			}
			else {
				BodyScript.effectTargetID.Add(50*(1-player));
				BodyScript.effectMotion.Add(40);	
			}
			
			BodyScript.effectTargetID.Add(player*50);
			BodyScript.effectMotion.Add(2);	
			
			BodyScript.messages.Clear();
		}
		
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			int f=0;
			int target=player;		
			int num=ManagerScript.getNumForCard(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardScr(x,target).BurstIcon==1 && ManagerScript.getCardType(x,target)==2)
					BodyScript.Targetable.Add(x+50*target);
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
		}
		
		
		field=ManagerScript.getFieldInt(ID,player);
	}
}

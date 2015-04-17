using UnityEngine;
using System.Collections;

public class WX03_030 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
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
		//effect
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			
			//my handDeath
			int target=player;
			int f=2;
			int num=ManagerScript.getFieldAllNum(f,target);
			if(f==3)num=3;
			
			int num_2=num;
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(7);
				}
			}
			
			//your handDeath
			target=1-player;
			num=ManagerScript.getFieldAllNum(f,target);
			
			int count=0;
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0){
					count++;
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(7);
				}
			}
			
			//draw
			for(int i=0;(i<num || i<num_2) && i<ManagerScript.getFieldAllNum(0,player);i++){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(2);
			}
			for(int i=0;(i<num || i<num_2) && i<ManagerScript.getFieldAllNum(0,1-player);i++){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(50*(1-player));
				BodyScript.effectMotion.Add(2);
			}
		}
		
		//update
		field=ManagerScript.getFieldInt(ID,player);
	}
}

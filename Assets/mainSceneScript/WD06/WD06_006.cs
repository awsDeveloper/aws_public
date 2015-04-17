using UnityEngine;
using System.Collections;

public class WD06_006 : MonoBehaviour {
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
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int f=5;
			int target=1-player;
			int num=ManagerScript.getNumForCard(f,target);
			
			int x=ManagerScript.getFieldRankID(f,num-1,target);
			if(x>=0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(x+50*target);
				BodyScript.effectMotion.Add(29);
				
				if(ManagerScript.getCardScr(x,target).BurstIcon!=1){
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(7);					
				}
				else{
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(30);
				}
			}
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

using UnityEngine;
using System.Collections;

public class WD06_015 : MonoBehaviour {
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
			int f=5;
			int target=player;
			int num=ManagerScript.getNumForCard(f,target);
			
			int x=ManagerScript.getFieldRankID(f,num-1,target);
			if(x>=0){
				BodyScript.Targetable.Add(x+50*target);
				
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(22);
			}
		}
		
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

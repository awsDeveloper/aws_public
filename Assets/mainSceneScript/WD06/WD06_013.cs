using UnityEngine;
using System.Collections;

public class WD06_013 : MonoBehaviour {
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
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			if(ManagerScript.getFieldAllNum(5,player)>0)BodyScript.DialogFlag=true;
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){			
				int f=5;
				int target=player;
				int num=ManagerScript.getNumForCard(f,target);
				
				int x=ManagerScript.getFieldRankID(f,num-1,target);
				if(x>=0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(7);
					BodyScript.effectTargetID.Add(50*target);
					BodyScript.effectMotion.Add(41);
				}
			}
			
			BodyScript.messages.Clear();
		}		
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

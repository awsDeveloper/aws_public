using UnityEngine;
using System.Collections;

public class WD06_002 : MonoBehaviour {
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
		if(ManagerScript.getFieldInt(ID,player)==4 && field!=4 && !BodyScript.BurstFlag){
			int f=5;
			int target=player;
			int num=ManagerScript.getNumForCard(f,target);
			
			for(int i=0;i<num && i<2 ;i++){
				int x=ManagerScript.getFieldRankID(f,num-i-1,target);
				if(x>=0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(44);
				}
			}			
		}		
		field=ManagerScript.getFieldInt(ID,player);
	}
}

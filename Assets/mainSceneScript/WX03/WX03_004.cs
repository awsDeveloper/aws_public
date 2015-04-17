using UnityEngine;
using System.Collections;

public class WX03_004 : MonoBehaviour {
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
		//change cost
		if(ManagerScript.getLrigColor(1-player)==5){
			BodyScript.Cost[0]=0;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=1;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=0;
		}
		else{
			BodyScript.Cost[0]=3;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=1;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=0;
		}
		
		//effect
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			for (int i = 0; i < 2 && i < ManagerScript.getFieldAllNum(2,1-player) ; i++) {
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add((1-player)*50);
				BodyScript.effectMotion.Add(32);
			}
		}
		
		//update
		field=ManagerScript.getFieldInt(ID,player);
	}
}

using UnityEngine;
using System.Collections;

public class WX03_005 : MonoBehaviour {
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

		BodyScript.attackArts=true;
	}
	// Update is called once per frame
	void Update () {
		//change cost
		if(ManagerScript.getLrigColor(1-player)==5){
			BodyScript.Cost[0]=0;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=1;
			BodyScript.Cost[5]=0;
		}
		else{
			BodyScript.Cost[0]=3;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=1;
			BodyScript.Cost[5]=0;
		}
		
		//effect
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int deckNum=ManagerScript.getFieldAllNum(0,player);
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(ManagerScript.getFieldRankID(0,deckNum-1,player)+50*player);
			BodyScript.effectMotion.Add(3);
		}
		
		//update
		field=ManagerScript.getFieldInt(ID,player);
	}
}

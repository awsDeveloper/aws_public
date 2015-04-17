using UnityEngine;
using System.Collections;

public class WD07_009_always : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	int rank=-1;

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
		//cip
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3){
			rank=ManagerScript.getRank(ID,player);
			rank=1-(rank-1);//正面のランクを得る
			
			ManagerScript.signiRockFlag[1-player,rank]=true;
		}
		
		//pig
		if(ManagerScript.getFieldInt(ID,player)!=3 && field==3){
			ManagerScript.signiRockFlag[1-player,rank]=false;
		}
		
		
		field=ManagerScript.getFieldInt(ID,player);
	
	}
}

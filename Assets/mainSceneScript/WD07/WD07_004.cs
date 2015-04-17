using UnityEngine;
using System.Collections;

public class WD07_004 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int LrigID=-1;

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
		if(LrigID!=ID && ManagerScript.getLrigID(player)==ID){
			ManagerScript.ionaFlag[1-player]=true;
		}
		
		//pig
		if(LrigID==ID && ManagerScript.getLrigID(player)!=ID){
			ManagerScript.ionaFlag[1-player]=false;	
		}
		
		
		LrigID=ManagerScript.getLrigID(player);
	
	}
}

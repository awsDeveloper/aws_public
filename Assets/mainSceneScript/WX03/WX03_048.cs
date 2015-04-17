using UnityEngine;
using System.Collections;

public class WX03_048 : MonoBehaviour {
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
		
		BodyScript.powerUpValue=3000;
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getTrashSummonID()==ID+50*player){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(ID+50*player);
			BodyScript.effectMotion.Add(34);
		}
	
	}
}

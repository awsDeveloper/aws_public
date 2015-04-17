using UnityEngine;
using System.Collections;

public class WD07_012_always : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;

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
		if(ManagerScript.getFieldInt(ID,player)==3){
			int counterID=ManagerScript.getCounterID();
			if(counterID!=-1){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(counterID);
				BodyScript.effectMotion.Add(5);
			}
		}
	}
}

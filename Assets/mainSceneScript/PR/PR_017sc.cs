using UnityEngine;
using System.Collections;

public class PR_017sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool upFlag=false;
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=5000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==3){
			int lrigID=ManagerScript.getLrigID(player);
			if(lrigID>=0){
				CardScript lrig=ManagerScript.getCardScr(lrigID,player);
				if(lrig.Level==4 && lrig.LrigType==1 && !upFlag){
					BodyScript.Power+=BodyScript.powerUpValue;
					upFlag=true;
				}
			}
		}
		else if(upFlag){
			BodyScript.Power-=BodyScript.powerUpValue;
			upFlag=false;
		}
	}
}

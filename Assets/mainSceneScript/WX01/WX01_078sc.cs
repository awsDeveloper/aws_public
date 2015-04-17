using UnityEngine;
using System.Collections;

public class WX01_078sc : MonoBehaviour {
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
		BodyScript.powerUpValue=3000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==3 && ManagerScript.getFieldAllNum(2,player)-ManagerScript.getFieldAllNum(2,1-player)>=2){
			if(!upFlag){
				ManagerScript.changeBasePower(ID,player,8000);
				upFlag=true;
			}
		}
		else if(upFlag){
            ManagerScript.changeBasePower(ID, player, 5000);
            upFlag = false;
		}
	}
}

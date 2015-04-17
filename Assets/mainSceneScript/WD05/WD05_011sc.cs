using UnityEngine;
using System.Collections;


public class WD05_011sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool upFlag=false;
	
	int size=5000;
	
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
		if(ManagerScript.getFieldInt(ID,player)==3 && ManagerScript.getFieldAllNum(7,player)>=10){
			if(!upFlag){
				upFlag=true;
                ManagerScript.alwaysChagePower(ID, player, size, ID, player);
			}
		}
        else if (upFlag)
        {
            upFlag = false;
            ManagerScript.powChanListChangerClear(ID,player);
        }

		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(player*50 );
			BodyScript.effectMotion.Add(2);
		}
		field=ManagerScript.getFieldInt(ID,player);	
	
	}
}

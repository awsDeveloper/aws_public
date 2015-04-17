using UnityEngine;
using System.Collections;

public class WX04_098 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool upFlag=false;
	
	// Use this for initialization
	void Start ()
	{
		Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;
		
		Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();
		
		BodyScript.powerUpValue=5000;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//always
		if(ManagerScript.getFieldInt(ID,player)==3 && ManagerScript.GetCharm(ID,player)!=-1){
            if (!upFlag)
            {
                ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower + BodyScript.powerUpValue);
                upFlag = true;
            }
        }
        else if (upFlag)
        {
            ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower);
            upFlag = false;
        }
        
        //UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}
}

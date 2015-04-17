using UnityEngine;
using System.Collections;

public class PR_057 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool chantFlag=false;
	bool RefleshedFlag=false;
	
	// Use this for initialization
	void Start ()
	{
		Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;
		
		Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//flag up
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
		{
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (50 * player);
			BodyScript.effectMotion.Add (22);

			chantFlag=true;
		}
		
		//flag down
		if(chantFlag && ManagerScript.getPhaseInt()==7)
		{
			chantFlag=false;
		}

		//effect
		if (chantFlag && !RefleshedFlag && ManagerScript.getRefleshedFlag(player))
		{
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (50 * ( 1 - player));
			BodyScript.effectMotion.Add (51);
		}

		//update
		field = ManagerScript.getFieldInt (ID, player);
		RefleshedFlag=ManagerScript.getRefleshedFlag(player);
	}
}

using UnityEngine;
using System.Collections;

public class PR_077 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool chantFlag=false;
	
	// Use this for initialization
	void Start ()
	{
		Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;
		
		Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();

		BodyScript.SpellCutIn=true;
		BodyScript.attackArts=true;
		BodyScript.notMainArts=true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//effect
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
		{
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (50 * player);
			BodyScript.effectMotion.Add (22);

			ManagerScript.setSpellCostDown (0, -3, 1-player, 0);
			
			if(ManagerScript.getLrigType(player)==3)
				ManagerScript.setSpellCostDown (0, -3, 1-player, 1);

			chantFlag=true;
		}

		//flag down
		if(chantFlag && ManagerScript.getPhaseInt()==7)
		{
			chantFlag=false;
		}

		//reset
		if(chantFlag && ManagerScript.getCostDownResetFlag(0))
		{
			ManagerScript.setSpellCostDown (0, -3, 1-player, 0);
			
		}

		if(chantFlag && ManagerScript.getCostDownResetFlag(1))
		{
			if(ManagerScript.getLrigType(player)==3)
				ManagerScript.setSpellCostDown (0, -3, 1-player, 1);
		}
		//update
		field = ManagerScript.getFieldInt (ID, player);
	}
}

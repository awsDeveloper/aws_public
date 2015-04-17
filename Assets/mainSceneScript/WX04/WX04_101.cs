using UnityEngine;
using System.Collections;

public class WX04_101 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;

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
		//ignition
		if (BodyScript.Ignition)
		{
			BodyScript.Ignition = false;
			
			if (ManagerScript.getIDConditionInt (ID, player) == 1)
			{
				int target = 1-player;
				int f = 3;
				int num = ManagerScript.getNumForCard (f, target);
				
				for (int i=num-1; i>=0; i--)
				{
					int x = ManagerScript.getFieldRankID (f, i, target);
					if (x >= 0){
						BodyScript.Targetable.Add(x+50*target);
					}
				}
				
				if(BodyScript.Targetable.Count>0)
				{
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (ID + 50 * player);
					BodyScript.effectMotion.Add (65);
					
					BodyScript.effectTargetID.Add (-1);
					BodyScript.effectMotion.Add (34);

					BodyScript.powerUpValue=-1000*ManagerScript.getLrigLevel(player);
				}
			}
		}	

		//burst
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (50*player);
			BodyScript.effectMotion.Add (2);
		}

		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}
}

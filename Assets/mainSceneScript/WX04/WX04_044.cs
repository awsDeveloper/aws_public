using UnityEngine;
using System.Collections;

public class WX04_044 : MonoBehaviour
{
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	bool costFlag = false;

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

			if (ManagerScript.getIDConditionInt (ID, player) == 1 && ManagerScript.getFieldAllNum(6,1-player)<=4)
			{
				costFlag = true;
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (ID + 50 * player);
				BodyScript.effectMotion.Add (8);
			}
		}

		if (BodyScript.effectTargetID.Count == 0 && costFlag)
		{
			costFlag = false;

			for (int i=0; i<3; i++)
			{
				int x = ManagerScript.getFieldRankID (3, i, 1 - player);
				if (x > 0 && ManagerScript.getCardPower (x, 1 - player) <= 8000)
				{
					BodyScript.Targetable.Add (x + (1 - player) * 50);
				}
			}

			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (5);
			}
		}
	}
}

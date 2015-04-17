using UnityEngine;
using System.Collections;

public class WX02_042sc : MonoBehaviour
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
		if (BodyScript.Ignition)
		{
			BodyScript.Ignition = false;
			int costNum = ManagerScript.getEnaColorNum (5, player);
			costNum += ManagerScript.MultiEnaNum (player);
			if (costNum >= 1 && ManagerScript.getIDConditionInt (ID, player) == 1 && ManagerScript.getFieldAllNum (3, player) < 3)
			{
				costFlag = true;
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (ID + player * 50);
				BodyScript.effectMotion.Add (17);
				BodyScript.Cost [0] = 0;
				BodyScript.Cost [1] = 0;
				BodyScript.Cost [2] = 0;
				BodyScript.Cost [3] = 0;
				BodyScript.Cost [4] = 0;
				BodyScript.Cost [5] = 1;
				BodyScript.effectTargetID.Add (ID + 50 * player);
				BodyScript.effectMotion.Add (8);
			}
		}
		if (costFlag && BodyScript.effectTargetID.Count==0)
		{
			costFlag = false;
			BodyScript.Targetable.Clear();

			int target = player;
			int f = 7;
			int num = ManagerScript.getFieldAllNum (f, target);
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0 && ManagerScript.getCardType (x, target) == 2)
					BodyScript.Targetable.Add (x + 50 * target);
			}
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectTargetID.Add (-2);
				BodyScript.effectMotion.Add (6);
			}
		}
	}
}

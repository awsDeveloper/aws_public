using UnityEngine;
using System.Collections;

public class SP01_009 : MonoBehaviour
{
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

        BodyScript.attackArts = true;
	}
	// Update is called once per frame
	void Update ()
	{
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
		{
			int target = player;
			int f = 9;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0
					&& ManagerScript.getCardType (x, target) == 1 
					&& ManagerScript.getCardScr (x, target).SerialNumString != BodyScript.SerialNumString)
					BodyScript.Targetable.Add (x + 50 * target);				
			}
			
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-2);
				BodyScript.effectMotion.Add (49);
			}			
		}
		field = ManagerScript.getFieldInt (ID, player);
	}
}

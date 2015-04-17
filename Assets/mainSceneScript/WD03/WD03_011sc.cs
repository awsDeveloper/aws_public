using UnityEngine;
using System.Collections;

public class WD03_011sc : MonoBehaviour
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
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (ManagerScript.getFieldInt (ID, player) == 3 && field != 3)
		{
			int handNum = ManagerScript.getFieldAllNum (2, 1 - player);
			if (handNum > 0)
			{
				for (int i=0; i<handNum; i++)
				{	
					int x = ManagerScript.getFieldRankID (2, i, 1 - player);
					if (ManagerScript.getCardLevel (x, 1 - player) == 1)
						BodyScript.Targetable.Add (x + 50 * (1 - player));
				}
				if (BodyScript.Targetable.Count > 0)
				{
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (50*(1-player));
					BodyScript.effectMotion.Add (66);
					BodyScript.effectTargetID.Add (-1);
					BodyScript.effectMotion.Add (19);
					BodyScript.effectTargetID.Add (50*(1-player));
					BodyScript.effectMotion.Add (67);
				}
			}
		}
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (player * 50);
			BodyScript.effectMotion.Add (2);
		}
		field = ManagerScript.getFieldInt (ID, player);
	}
}

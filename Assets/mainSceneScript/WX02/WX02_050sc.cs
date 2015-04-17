using UnityEngine;
using System.Collections;

public class WX02_050sc : MonoBehaviour
{
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool chantFlag_1 = false;
	bool chantFlag_2 = false;
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
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
		{
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (50 * player);
			BodyScript.effectMotion.Add (22);
			BodyScript.AntiCheck = true;
			chantFlag_1 = true;
		}
		if (BodyScript.effectTargetID.Count == 0 && chantFlag_1)
		{
			BodyScript.Targetable.Clear ();
			chantFlag_1 = false;
			if (!BodyScript.AntiCheck)
			{
				chantFlag_2 = true;
				int deckNum = ManagerScript.getFieldAllNum (0, player);			
				for (int i=0; i<deckNum; i++)
				{
					int x = ManagerScript.getFieldRankID (0, i, player);
					if (checkClass_1 (x, player))
					{
						BodyScript.Targetable.Add (x + 50 * player);
					}
				}
				if (BodyScript.Targetable.Count > 0)
				{
					BodyScript.effectTargetID.Add (-2);
					BodyScript.effectMotion.Add (16);
				}				
			}
			else
				BodyScript.AntiCheck = false;
		}

		if (BodyScript.effectTargetID.Count == 0 && chantFlag_2)
		{
			BodyScript.Targetable.Clear ();
			chantFlag_2 = false;
			int deckNum = ManagerScript.getFieldAllNum (0, player);			
			for (int i=0; i<deckNum; i++)
			{
				int x = ManagerScript.getFieldRankID (0, i, player);
				if (checkClass_2 (x, player))
				{
					BodyScript.Targetable.Add (x + 50 * player);
				}
			}
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectTargetID.Add (-2);
				BodyScript.effectMotion.Add (16);
			}				
		}
		
		field = ManagerScript.getFieldInt (ID, player);	
	}

	bool checkClass_1 (int x, int cplayer)
	{
        return ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_アーム);
	}

	bool checkClass_2 (int x, int cplayer)
	{
        return ManagerScript.checkClass(x, cplayer, cardClassInfo.精像_天使);
    }
}

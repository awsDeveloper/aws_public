using UnityEngine;
using System.Collections;

public class WX01_028sc : MonoBehaviour
{
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool chantFlag = false;
	bool attackNowCheck = false;
	bool checkFlag=false;

	
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
			BodyScript.effectTargetID.Add (ID + 50 * player);
			BodyScript.effectMotion.Add (22);
			BodyScript.AntiCheck = true;
			checkFlag=true;
		}

		if (checkFlag && BodyScript.effectTargetID.Count==0)
		{
			checkFlag=false;

			if (!BodyScript.AntiCheck){
				chantFlag = true;
			}
			else
			{
				chantFlag = false;
				BodyScript.AntiCheck = false;
			}
		}

		if (chantFlag)
		{
			int lrigNum = ManagerScript.getFieldAllNum (4, player);

			CardScript lrig = ManagerScript.getCardScr (ManagerScript.getFieldRankID (4, lrigNum - 1, player), player);

			if (lrig.AttackNow && !attackNowCheck)
			{
				for (int i=0; i<3; i++)
				{
					int x = ManagerScript.getFieldRankID (3, i, player);
					if (x >= 0)
						BodyScript.Targetable.Add (x + 50 * player);
				}

				if (BodyScript.Targetable.Count > 0)
				{
					BodyScript.DialogFlag = true;
				}
			}

			attackNowCheck = lrig.AttackNow;

			if (ManagerScript.getPhaseInt () == 7)
				chantFlag = false;
		}
		
		//receive
		if (BodyScript.messages.Count > 0)
		{
			if (BodyScript.messages [0].Contains ("Yes"))
			{
				int lrigID = ManagerScript.getLrigID (player);
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (7);
				BodyScript.effectTargetID.Add (lrigID + 50 * player);
				BodyScript.effectMotion.Add (9);
			}
			else
				BodyScript.Targetable.Clear ();
			BodyScript.messages.Clear ();
		}
		
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			int opp = 1 - player;
			for (int i=0; i<3; i++)
			{
				int x = ManagerScript.getFieldRankID (3, i, opp);
				if (x >= 0 && ManagerScript.getSigniConditionInt (i, opp) == 1)
				{
					BodyScript.effectTargetID.Add (x + 50 * opp);
				}
			}
			int rank = ManagerScript.getFieldAllNum (4, opp);
			BodyScript.effectTargetID.Add (ManagerScript.getFieldRankID (4, rank - 1, opp) + 50 * opp);
			for (int i=0; i<BodyScript.effectTargetID.Count; i++)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectMotion.Add (8);
			}
		}
		field = ManagerScript.getFieldInt (ID, player);
	}
}

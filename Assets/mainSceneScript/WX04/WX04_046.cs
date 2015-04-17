using UnityEngine;
using System.Collections;

public class WX04_046 : MonoBehaviour
{
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field=-1;
	bool UpFlag=false;

	// Use this for initialization
	void Start ()
	{
		GameObject Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;

		GameObject Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//always
		if (ManagerScript.getFieldInt (ID, player) == 3)
		{
			if (!UpFlag && !ManagerScript.ACGFlag [1 - player])
			{
				UpFlag = true;
				ManagerScript.ACGFlag [1 - player] = true;
			}
		}
		else if (UpFlag)
		{
			UpFlag = false;
			ManagerScript.ACGFlag [1 - player] = false;
		}

		//ignition
		if (BodyScript.Ignition)
		{
			BodyScript.Ignition = false;
			
			if (ManagerScript.getIDConditionInt (ID, player) == 1)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (ID + 50 * player);
				BodyScript.effectMotion.Add (8);
			

				for (int i=0; i<3; i++)
				{
					int x = ManagerScript.getFieldRankID (3, i, 1 - player);
					if (x > 0)
					{
						BodyScript.Targetable.Add (x + (1 - player) * 50);
					}
				}
			
				if (BodyScript.Targetable.Count > 0)
				{
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (-1);
					BodyScript.effectMotion.Add (27);
				}
			}
		}

        //burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag)
		{
			for (int i=0; i<3; i++)
			{
				int x = ManagerScript.getFieldRankID (3, i, 1 - player);
				if (x > 0)
				{
					BodyScript.Targetable.Add (x + (1 - player) * 50);
				}
			}
			
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (60);
			}

			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (50*player);
			BodyScript.effectMotion.Add (2);

		}

		//UpDate
		field=ManagerScript.getFieldInt(ID,player);
	}
}

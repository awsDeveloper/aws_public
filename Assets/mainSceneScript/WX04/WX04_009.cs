using UnityEngine;
using System.Collections;

public class WX04_009 : MonoBehaviour
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
		if (ManagerScript.getFieldInt (ID, player) == 4 && field != 4)
		{
			int f = 6;
			int target = 1 - player;
			int num = ManagerScript.getFieldAllNum (f, target);
			if (f == 3)
				num = 3;
			
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0 && ManagerScript.getCardScr (x, target).MultiEnaFlag == true)
				{
					BodyScript.Targetable.Add (x + 50 * target);
				}
			}
			
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectSelecter = target;
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (7);
				BodyScript.effectTargetID.Add (50*target);
				BodyScript.effectMotion.Add (20);
			}
		}

		field = ManagerScript.getFieldInt (ID, player);	
	}
}
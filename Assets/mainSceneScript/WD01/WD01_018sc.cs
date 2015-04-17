using UnityEngine;
using System.Collections;

public class WD01_018sc : MonoBehaviour
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
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8)
		{
			BodyScript.effectFlag = true;
			if (BodyScript.BurstFlag == true)
			{
				BodyScript.effectTargetID.Add (50 * player);
				BodyScript.effectMotion.Add (26);
			}
			else
			{
				BodyScript.effectTargetID.Add (player * 50);
				BodyScript.effectMotion.Add (2);
			}
		}
		field = ManagerScript.getFieldInt (ID, player);
	}
}

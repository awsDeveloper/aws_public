using UnityEngine;
using System.Collections;

public class PR_078 : MonoBehaviour
{
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool equipFlag = false;
	
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
		//check equip
		if (ManagerScript.getFieldInt (ID, player) == 3 && ManagerScript.getFieldAllNum (3, 1 - player) == 3)
		{
			if (!equipFlag && !BodyScript.lancer)
			{
				equipFlag = true;
				BodyScript.lancer = true;
			}
		} 
		else
		{
			if (equipFlag)
			{
				equipFlag = false;
				BodyScript.lancer = false;
			}
		}
	}
}
using UnityEngine;
using System.Collections;

public class SP02_010 : MonoBehaviour {
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

		BodyScript.attackArts=true;
	}
	// Update is called once per frame
	void Update ()
	{
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
		{
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (50*player);
			BodyScript.effectMotion.Add (22);

			for (int i = 0; i < 50; i++) {
				GameObject card=ManagerScript.getFront(i,player);

				if(card!=null)
					card.AddComponent<SP02_010_eq>();
			}
		}			

		field = ManagerScript.getFieldInt (ID, player);
	}
}

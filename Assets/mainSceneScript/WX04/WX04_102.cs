using UnityEngine;
using System.Collections;

public class WX04_102 : MonoBehaviour {
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
		if( ManagerScript.getWentTrashID() == ID + 50 * player )
		{
			int from = ManagerScript.getWentTrashFrom();

			if(from == 0 || from == 2)
			{
				int target=player;
				int f=3;
				int num=ManagerScript.getNumForCard(f,target);
				
				for (int i = 0; i < num; i++) {
					int x=ManagerScript.getFieldRankID(f,i,target);
					
					if(x>=0 && ManagerScript.GetCharm(x,target)==-1)
					{
						BodyScript.Targetable.Add(x+50*target);
					}
				}
				
				if(BodyScript.Targetable.Count>0)
				{
					BodyScript.effectFlag = true;
					BodyScript.CharmizeID = ID + 50 * player;

					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(63);
				}
			}
		}
		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}
}

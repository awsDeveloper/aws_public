using UnityEngine;
using System.Collections;

public class WX04_072 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	bool costFlag = false;
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
		//ignition
		if (BodyScript.Ignition)
		{
			BodyScript.Ignition = false;
			
			if (true)
			{
				costFlag = true;
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (ID + 50 * player);
				BodyScript.effectMotion.Add (65);
			}
		}

		//after cost
		if (BodyScript.effectTargetID.Count == 0 && costFlag)
		{
			costFlag = false;

			int target=1-player;
			int f=6;
			int num=ManagerScript.getNumForCard(f,target);
			
			for (int i = 0; i < num; i++) {
				int x=ManagerScript.getFieldRankID(f,i,target);
				
				if(x>=0 && ManagerScript.getCardScr(x,target).MultiEnaFlag){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0)
			{
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(7);
				BodyScript.effectTargetID.Add(50*target);
				BodyScript.effectMotion.Add(20);
			}
		}

		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(2);
		}

		//update
		field = ManagerScript.getFieldInt (ID, player);	


	}
}

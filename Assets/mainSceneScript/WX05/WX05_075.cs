using UnityEngine;
using System.Collections;

public class WX05_075 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool costFlag=false;
	
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
		//cip
		if (ManagerScript.getFieldInt (ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
		{
			targetIn(7);
			targetIn(2);

			if(BodyScript.Targetable.Count>0){
				BodyScript.Targetable.Clear();
				BodyScript.DialogFlag = true;
			}
		}

		//burst
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add (player * 50);
			BodyScript.effectMotion.Add (26);
		}

		//receive
		if (BodyScript.messages.Count > 0)
		{
			if (BodyScript.messages [0].Contains ("Yes"))
			{
				int target = player;
				int f = 2;
				int num = ManagerScript.getFieldAllNum (f, target);
				
				for (int i = 0; i < num; i++)
				{
					int x = ManagerScript.getFieldRankID (f, i, target);
					
					if (x >= 0 && checkClass (x, target))
					{
						BodyScript.Targetable.Add (x + 50 * target);
					}
				}
				
				if (BodyScript.Targetable.Count > 0)
				{
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (-1);
					BodyScript.effectMotion.Add (19);

					costFlag = true;
				}
			}
			BodyScript.messages.Clear ();
		}

		//cip after cost
		if(costFlag && BodyScript.effectTargetID.Count==0)
		{
			costFlag=false;
			BodyScript.Targetable.Clear();

			targetIn(7);

			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-2);
				BodyScript.effectMotion.Add (6);
			}
		}
		
		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}
	
	bool checkClass (int x, int cplayer)
	{
		if (x < 0)
			return false;
		int[] c = ManagerScript.getCardClass (x, cplayer);
		return (c [0] == 3 && c [1] == 1);
	}

	void targetIn(int f){
		int target = player;
		int num = ManagerScript.getNumForCard(f,target);
		
		for (int i = 0; i < num; i++)
		{
			int x = ManagerScript.getFieldRankID (f, i, target);
			
			if (x >= 0 && checkClass(x,target) && ManagerScript.getCardLevel(x,target)>=3) 
			{
				BodyScript.Targetable.Add (x + 50 * target);
			}
		}
	}
}

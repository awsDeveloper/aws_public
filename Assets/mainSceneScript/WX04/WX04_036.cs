using UnityEngine;
using System.Collections;

public class WX04_036 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	int count = 0;
	bool chantFlag = false;
	bool costFlag = false;

	
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
		//chant
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
		{
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (ID + 50 * player);
			BodyScript.effectMotion.Add (22);
			BodyScript.AntiCheck = true;
			chantFlag = true;
		}
		
		//after chant 
		if (BodyScript.effectTargetID.Count == 0 && chantFlag)
		{
			chantFlag = false;
			if (!BodyScript.AntiCheck)
			{
				int target = player;
				for (int i=0; i<3; i++)
				{
					int x = ManagerScript.getFieldRankID (3, i, target);
					if (checkClass (x, target))
						BodyScript.Targetable.Add (x + 50 * target);
				}

				if (BodyScript.Targetable.Count > 0)
				{
					count = BodyScript.Targetable.Count;
					BodyScript.DialogNum = 1;
					BodyScript.DialogFlag = true;
					BodyScript.DialogCountMax = count;
				}
			}
			else
				BodyScript.AntiCheck = false;
		}

		//after cost
		if (BodyScript.effectTargetID.Count == 0 && costFlag)
		{
			costFlag = false;

			int target = player;
			int f = 0;
			int num=ManagerScript.getNumForCard(f,target);

			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0 && checkClass(x,target))
					BodyScript.Targetable.Add (x + 50 * target);
			}

			if (BodyScript.Targetable.Count > 0)
			{
				for (int i=0; i<count && i<BodyScript.Targetable.Count; i++)
				{
					BodyScript.effectTargetID.Add (-2);
					BodyScript.effectMotion.Add (31);
				}
			}
		}

		//burst
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			int target = player;
			int f = 0;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=num-1; i>num-4; i--)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0){
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (x+50*target);
					BodyScript.effectMotion.Add (54);
				}

			}

			int c=BodyScript.effectTargetID.Count;

			//goHand
			for(int i=0;i<c;i++)
			{
				int x=BodyScript.effectTargetID[i]%50;

				if(checkClass(x,target))
				{
					BodyScript.effectTargetID.Add (x+50*target);
					BodyScript.effectMotion.Add (16);
				}
			}

			//goTrash
			for(int i=0;i<c;i++)
			{
				int x=BodyScript.effectTargetID[i]%50;
				
				if(!checkClass(x,target))
				{
					BodyScript.effectTargetID.Add (x+50*target);
					BodyScript.effectMotion.Add (7);
				}
			}

		}
		
		//receive
		if (BodyScript.messages.Count > 0)
		{
			if (int.TryParse (BodyScript.messages [0], out count))
			{
				if (count > 0)
				{					
					for (int i=0; i<count; i++)
					{
						BodyScript.effectTargetID.Add (-1);
						BodyScript.effectMotion.Add (5);
					}
					
					BodyScript.effectFlag = true;
					costFlag = true;
				}
			}

			if(!BodyScript.effectFlag)
				BodyScript.Targetable.Clear ();
			
			BodyScript.messages.Clear ();
		}

		field = ManagerScript.getFieldInt (ID, player);	
	}

	bool checkClass (int x, int cplayer)
	{
		if (x < 0)
			return false;
		int[] c = ManagerScript.getCardClass (x, cplayer);
		return (c [0] == 4 && c [1] == 2);
	}
}

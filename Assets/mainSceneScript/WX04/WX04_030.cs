using UnityEngine;
using System.Collections;

public class WX04_030 : MonoBehaviour
{
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
		BodyScript.Cost[1]=3-getClassNum(player);

		//chant
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
		{
			int target = 1-player;
			int f = 3;
			int num = ManagerScript.getNumForCard(f,target);
			
			for (int i = 0; i < num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				
				if (x >= 0)
				{
					BodyScript.Targetable.Add (x + 50 * target);
				}
			}
		
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectSelecter=player;
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (25);
				BodyScript.effectTargetID.Add (target*50);
				BodyScript.effectMotion.Add (24);
			}
		}

		//burst
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			BodyScript.DialogFlag = true;
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
					BodyScript.effectSelecter=player;
				}
			}
			BodyScript.messages.Clear ();
		}

		if(costFlag && BodyScript.effectTargetID.Count==0)
		{
			costFlag=false;
			BodyScript.Targetable.Clear();

			int target = 1 - player;
			int f = 3;
			int num = ManagerScript.getNumForCard(f,target);
			
			for (int i = 0; i < num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				
				if (x >= 0)
				{
					BodyScript.Targetable.Add (x + 50 * target);
				}
			}
			
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (7);
				BodyScript.effectSelecter=1-player;
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
		return (c [0] == 3 && c [1] == 2);
	}

	int getClassNum(int target){
		int num=0;
		for (int i = 0; i < 3; i++)
		{
			int x=ManagerScript.getFieldRankID(3,i,target);
			if(x>=0 && checkClass(x,target))
				num++;
		}
		return num;
	}
}

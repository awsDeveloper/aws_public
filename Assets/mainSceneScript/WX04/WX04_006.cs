using UnityEngine;
using System.Collections;

public class WX04_006 : MonoBehaviour
{
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	bool costFlag_1 = false;
	bool costFlag_2 = false;
	bool cipCostFlag = false;
	bool turnIgniFlag = false;
	
	// Use this for initialization
	void Start ()
	{
		Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;
		BodyScript.powerUpValue = 5000;
		
		Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();
		
		//dialog
		BodyScript.checkStr.Add ("バニッシュ");
		BodyScript.checkStr.Add ("修復");
		
		for (int i=0; i<BodyScript.checkStr.Count; i++)
		{
			BodyScript.checkBox.Add (false);		
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (ID == ManagerScript.getLrigID (player))
		{			
			//ignition
			if (BodyScript.Ignition)
			{
				BodyScript.Ignition = false;
				
				bool[] flag = new bool[2];
				int target = 1 - player;
				int[] count = new int[2]{0,0};
				
				//ignition check 1
				for (int i=0; i<3; i++)
				{
					int x = ManagerScript.getFieldRankID (3, i, target);
					
					if (x >= 0)
						count [0] = 1;
					
				}
				
				target = player;
				int f = 2;
				int num = ManagerScript.getNumForCard (f, target);
				
				for (int i=0; i<num; i++)
				{
					int x = ManagerScript.getFieldRankID (f, i, target);
					
					if (x >= 0 && ManagerScript.getSigniColor (x, target) == 5)
						count [0] += 10;				
				}
				
				
				if (count [0] % 10 == 1 && count [0] > 20)
					flag [0] = true;
				
				//ignition check 2
				if (!turnIgniFlag)
				{
					ManagerScript.targetableExceedIn (player, BodyScript);
					if (BodyScript.Targetable.Count >= 2)
					{
						flag [1] = true;
						BodyScript.Targetable.Clear ();
					}
				}
				
				//select root
				if (flag [0] && flag [1])
				{
					BodyScript.DialogFlag = true;
					BodyScript.DialogNum = 2;
					BodyScript.DialogCountMax = 1;
				}
				else if (flag [0])
				{
					effect_1 ();
				}
				else if (flag [1])
				{
					effect_2 ();
				}
				
			}
		}
		
		//cip
		if (ManagerScript.getCipID () == ID + 50 * player)
		{
			BodyScript.Cost [0] = 0;
			BodyScript.Cost [1] = 0;
			BodyScript.Cost [2] = 0;
			BodyScript.Cost [3] = 0;
			BodyScript.Cost [4] = 0;
			BodyScript.Cost [5] = 1;
			
			if (ManagerScript.checkCost (ID, player) && ManagerScript.getFieldAllNum (7, player) > 0)
			{
				BodyScript.DialogFlag = true;
				BodyScript.DialogNum = 0;
			}
		}
		
		//receive
		if (BodyScript.messages.Count > 0)
		{
			if (BodyScript.messages [0].Contains ("Yes"))
			{
				
				switch (BodyScript.DialogNum)
				{
					
				case 0:
					cipCostFlag = true;
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (ID + player * 50);
					BodyScript.effectMotion.Add (17);
					break;
					
				case 2:
					if (BodyScript.checkBox [0])
						effect_1 ();
					else if (BodyScript.checkBox [1])
						effect_2 ();
					break;
				}
			}
			
			BodyScript.messages.Clear ();
		}
		
		//cip after cost
		if (BodyScript.effectTargetID.Count == 0 && cipCostFlag)
		{
			cipCostFlag = false;
			int target = player;
			int f = 7;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0 && ManagerScript.getCardType (x, target) == 2)
					BodyScript.Targetable.Add (x + 50 * target);				
			}
			
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-2);
				BodyScript.effectMotion.Add (16);
			}			
		}
		
		//ignition 1 after cost
		if (BodyScript.effectTargetID.Count == 0 && costFlag_1)
		{
			costFlag_1 = false;
			BodyScript.Targetable.Clear ();
			
			int target = 1 - player;
			int f = 3;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0)
					BodyScript.Targetable.Add (x + 50 * target);				
			}
			
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (5);
			}
		}
		
		//ignition 2 after cost
		if (BodyScript.effectTargetID.Count == 0 && costFlag_2)
		{
			costFlag_2 = false;
			BodyScript.Targetable.Clear ();
			
			int target = player;
			int f = 3;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0 && ManagerScript.getSigniColor(x,target)==5)
					BodyScript.Targetable.Add (x + 50 * target);		
			}
			
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (7);
				BodyScript.effectTargetID.Add (50*target);
				BodyScript.effectMotion.Add (41);
				turnIgniFlag = true;
			}
		}
		
		//update
		if (ManagerScript.getPhaseInt () == 7 && ManagerScript.getTurnPlayer () == player)
			turnIgniFlag = false;
	}
	
	void effect_1 ()
	{
		
		int target = player;
		int f = 2;
		int num = ManagerScript.getNumForCard (f, target);
		
		for (int i=0; i<num; i++)
		{
			int x = ManagerScript.getFieldRankID (f, i, target);
			if (x >= 0 && ManagerScript.getSigniColor(x,target)==5)
				BodyScript.Targetable.Add (x + 50 * target);		
		}
		
		if (BodyScript.Targetable.Count >= 2)
		{
			costFlag_1 = true;
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (-1);
			BodyScript.effectMotion.Add (19);			
			BodyScript.effectTargetID.Add (-1);
			BodyScript.effectMotion.Add (19);			
		}
		else BodyScript.Targetable.Clear();
	}
	
	void effect_2 ()
	{
		ManagerScript.targetableExceedIn (player, BodyScript);
		
		if (BodyScript.Targetable.Count >= 2)
		{
			costFlag_2 = true;
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (-2);
			BodyScript.effectMotion.Add (50);
			BodyScript.effectTargetID.Add (-2);
			BodyScript.effectMotion.Add (50);
		}
	}
	
	bool checkClass (int x, int cplayer)
	{
		if (x < 0)
			return false;
		int[] c = ManagerScript.getCardClass (x, cplayer);
		return (c [0] == 1 && (c [1] == 0 || c [1] == 1));
	}
}

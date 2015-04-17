using UnityEngine;
using System.Collections;

public class WX02_027sc : MonoBehaviour
{
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	int count = 0;
	int signiNum = 0;
	int weaponNum = 0;
	bool chantFlag = false;
	bool afterEffect_1 = false;
	bool afterEffect_2 = false;
	
	
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
		signiNum = ManagerScript.getFieldAllNum (3, 1 - player);
		calWeapon ();

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
					count = (signiNum < weaponNum) ? signiNum : weaponNum;	
					BodyScript.DialogNum = 1;
					BodyScript.DialogFlag = true;
					BodyScript.DialogCountMax = count;
				}
			}
			else
				BodyScript.AntiCheck = false;
		}

		if (BodyScript.effectTargetID.Count == 0 && afterEffect_1)
		{
			afterEffect_1 = false;
			BodyScript.Targetable.Clear ();
			int target = 1 - player;
			int f = 3;
			for (int i=0; i<3; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x > 0)
					BodyScript.Targetable.Add (x + 50 * target);
			}
			if (BodyScript.Targetable.Count > 0)
			{
				for (int i=0; i<count && i<BodyScript.Targetable.Count; i++)
				{
					BodyScript.effectTargetID.Add (-1);
					BodyScript.effectMotion.Add (5);
				}
			}
		}

		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			if (signiNum > 0)
			{
				BodyScript.DialogNum = 0;
				BodyScript.DialogFlag = true;
			}
		}
		
		//receive
		if (BodyScript.messages.Count > 0)
		{
			if (BodyScript.messages [0].Contains ("Yes"))
			{
				int target = 1 - player;
				for (int i=0; i<3; i++)
				{
					int x = ManagerScript.getFieldRankID (3, i, target);
					if (x >= 0)
						BodyScript.Targetable.Add (x + 50 * target);
				}
				if (BodyScript.Targetable.Count > 0)
				{
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (-1);
					BodyScript.effectMotion.Add (5);
					afterEffect_2 = true;
				}
			}
			else if (int.TryParse (BodyScript.messages [0], out count))
			{
				
				if (count > 0)
				{
					
					for (int i=0; i<count; i++)
					{
						BodyScript.effectTargetID.Add (-1);
						BodyScript.effectMotion.Add (7);
					}
					
					BodyScript.effectFlag = true;
					afterEffect_1 = true;
				}
				else
					BodyScript.Targetable.Clear ();
			}
			else
				BodyScript.Targetable.Clear ();
			
			BodyScript.messages.Clear ();
		}
		
		if (BodyScript.effectTargetID.Count == 0 && afterEffect_2)
		{
			afterEffect_2 = false;
			BodyScript.Targetable.Clear ();
			int target = player;
			int f = 2;
			int num = ManagerScript.getFieldAllNum (f, target);
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x > 0)
					BodyScript.Targetable.Add (x + 50 * target);
			}
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (19);
			}
		}
		field = ManagerScript.getFieldInt (ID, player);	
	}
	
	void calWeapon ()
	{
		weaponNum = 0;
		int target = player;
		for (int i=0; i<3; i++)
		{
			int x = ManagerScript.getFieldRankID (3, i, target);
			if (checkClass (x, target))
				weaponNum++;
		}
	}
	
	bool checkClass (int x, int cplayer)
	{
        if (ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_アーム))
            return true;

        return ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_ウェポン);
    }
	
		

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX05_005 : MonoBehaviour
{
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;

	List<int> equipList = new List<int> ();
	bool firstChecked = false;

	bool costFlag=false;

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
		//useLimit
		BodyScript.useLimit = trashColorNum(player,5) < 10;

		//checkEquip
		checkEquip ();


		//attackArts
		BodyScript.attackArts = ManagerScript.getFieldAllNum (4, player) >= 6 && ManagerScript.getLrigID (player) == ID;

		if (BodyScript.UseAttackArts)
		{
			BodyScript.UseAttackArts = false;

			ManagerScript.targetableExceedIn (player, BodyScript);

			if (BodyScript.Targetable.Count >= 5)
			{
				BodyScript.effectFlag = true;

				for (int i=0; i<5; i++)
				{
					BodyScript.effectTargetID.Add (-2);
					BodyScript.effectMotion.Add (50);
				}


				int target = 1 - player;
				int f = 3;
				int num = ManagerScript.getNumForCard (f, target);

				for (int i = 0; i < num; i++)
				{
					int x = ManagerScript.getFieldRankID (f, i, target);
					if (x >= 0)
					{
						BodyScript.effectTargetID.Add (x + 50 * target);
						BodyScript.effectMotion.Add ((int)Motions.EffectDown);
					}
				}

				BodyScript.effectTargetID.Add( ManagerScript.getLrigID(target) + 50*target);
                BodyScript.effectMotion.Add((int)Motions.EffectDown);
			}
			else 
				BodyScript.Targetable.Clear ();
		}


		//ignition
		if(BodyScript.Ignition){
			BodyScript.Ignition=false;

			BodyScript.changeColorCost(5,1);

			if(ManagerScript.checkCost(ID, player)){
				int target = player;
				int f = 6;
				int num = ManagerScript.getNumForCard(f,target);

				for (int i = 0; i < num; i++) {
					int x = ManagerScript.getFieldRankID(f,i,target);

					if(x>=0 && ManagerScript.getCardColor(x,target) == 5)
						BodyScript.Targetable.Add(x+50*target);
				}

				if(BodyScript.Targetable.Count>0)
				{
					BodyScript.effectFlag=true;

					BodyScript.effectTargetID.Add(ID + 50*player);
					BodyScript.effectMotion.Add(17);

					BodyScript.PayedCostEnable=true;

					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(65);

					BodyScript.effectTargetID.Add( 50 * player);
					BodyScript.effectMotion.Add(20);

					costFlag = true;
				}
			}
		}

		//targetable からエナコストで払ったやつを抜く
		if(BodyScript.PayedCostEnable && BodyScript.PayedCostList.Count > 0)
		{
			BodyScript.Targetable.Remove( BodyScript.PayedCostList[0] );
			BodyScript.PayedCostList.RemoveAt(0);

			if(BodyScript.Targetable.Count == 0)
				costFlag=false;
		}

		//after cost
		if(costFlag && BodyScript.effectTargetID.Count == 0)
		{
			costFlag=false;
			BodyScript.PayedCostEnable = false;

			int target = 1-player;
			int f = 3;
			int num = ManagerScript.getNumForCard(f,target);
			
			for (int i = 0; i < num; i++) {
				int x = ManagerScript.getFieldRankID(f,i,target);
				
				if(x>=0)
					BodyScript.Targetable.Add(x+50*target);
			}

			if(BodyScript.Targetable.Count>0)
			{
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(7);
			}
		}

		//update
		field = ManagerScript.getFieldInt (ID, player);
	}
	
	void equip (int x, int eplayer)
	{
		if (x < 0)
			return;

		if( ManagerScript.alwaysChangeColor(x, eplayer, 5, ID, player) )
			equipList.Add (x + 50 * eplayer);
	}

	bool checkTarget (int x, int eplayer)
	{
		CardScript sc = ManagerScript.getCardScr (x, eplayer);
		return ManagerScript.getFieldInt (x, eplayer) != 6 && sc.Type == 2;
	}

	bool isAfterEquip (int x, int eplayer)
	{
		CardScript sc = ManagerScript.getCardScr (x, eplayer);
		return  sc.CardColor == 5;
	}
	
	void dequip (int index)
	{
		if (index >= equipList.Count)
			return;

		int px = equipList [index];
		int x = px % 50;
		int eplayer = px / 50;

		if(ManagerScript.alwaysReturnColor(x, eplayer, ID, player))
			equipList.RemoveAt (index);
	}
	
	void checkEquip ()
	{
		//requip check
		if (ManagerScript.requipFlag)
			equipClear ();

		//check situation
		if (ManagerScript.getLrigID (player) == ID)
		{
			//全カード対象
			for (int i=0; i<100; i++)
			{
				int x = i % 50;
				int target = i / 50;

				//checkTarget
				if (checkTarget (x, target) && !isAfterEquip (x, target) && !checkExist (x, target))
					equip (x, target);

			}

			//firstChecked
			firstChecked = true;
		}
		else
		{
			equipClear ();
		}

		//equip target check
		if (equipList.Count > 0)
		{
			for (int i=0; i<equipList.Count; i++)
			{
				int x = equipList [i] % 50;
				int target = equipList [i] / 50;

				if (!checkTarget (x, target))
				{
					dequip (i);
					i--;
				}				
			}
		}
	}

	void equipClear ()
	{
		while (equipList.Count>0)
		{
			dequip (0);
		}
		
		firstChecked = false;
	}

	bool checkExist (int x, int player)
	{
		if (!firstChecked)
			return false;

		for (int i=0; i<equipList.Count; i++)
		{
			if (x + 50 * player == equipList [i])
				return true;
		}

		return false;
	}

	int trashColorNum(int plaer, int color)
	{
		int num=0;
		int count=ManagerScript.getFieldAllNum(7,plaer);

		for (int i = 0; i < count; i++)
		{
			int x=ManagerScript.getFieldRankID(7,i,plaer);

			if(ManagerScript.getCardColor(x,plaer)==color)
				num++;
		}

		return num;
	}
}

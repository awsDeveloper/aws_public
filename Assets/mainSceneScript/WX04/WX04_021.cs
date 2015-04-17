using UnityEngine;
using System.Collections;

public class WX04_021 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	bool costFlag_1 = false;
	bool costFlag_2 = false;
	bool costFlag_3 = false;
	bool[] turnIgniFlag = new bool[2];
	
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
		BodyScript.checkStr.Add ("デッキデス");
		BodyScript.checkStr.Add ("サルベージ");
		BodyScript.checkStr.Add ("バニッシュ");
		
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

				if(ManagerScript.getCharmNum(player)>0)
				{
					BodyScript.DialogFlag = true;
					BodyScript.DialogNum = 2;
					BodyScript.DialogCountMax = 1;
				}
			}
		}

		//receive
		if (BodyScript.messages.Count > 0)
		{
			if (BodyScript.messages [0].Contains ("Yes"))
			{
				if (BodyScript.checkBox [0] && !turnIgniFlag[0])
					effect_1 ();
				else if (BodyScript.checkBox [1] && !turnIgniFlag[1])
					effect_2 ();
				else if (BodyScript.checkBox [2])
					effect_3 ();
			}
			
			BodyScript.messages.Clear ();
		}
		
		//ignition 1 after cost
		if (BodyScript.effectTargetID.Count == 0 && costFlag_1)
		{
			costFlag_1 = false;
			BodyScript.Targetable.Clear ();

			//相手
			int target = 1 - player;
			int f = 0;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=0; i<3; i++)
			{
				int x = ManagerScript.getFieldRankID (f, num-1-i, target);
				if (x >= 0){
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (x+50*target);
					BodyScript.effectMotion.Add (7);
				}
			}

			//自分
			target=player;
			num=ManagerScript.getNumForCard (f, target);

			for (int i=0; i<3; i++)
			{
				int x = ManagerScript.getFieldRankID (f, num-1-i, target);
				if (x >= 0){
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (x+50*target);
					BodyScript.effectMotion.Add (7);
				}
			}

			if(BodyScript.effectFlag)
				turnIgniFlag[0]=true;
		}
		
		//ignition 2 after cost
		if (BodyScript.effectTargetID.Count == 0 && costFlag_2)
		{
			costFlag_2 = false;
			BodyScript.Targetable.Clear ();
			
			int target = player;
			int f = 7;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0 && ManagerScript.getSigniColor (x, target) == 5)
					BodyScript.Targetable.Add (x + 50 * target);		
			}
			
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-2);
				BodyScript.effectMotion.Add (16);
				turnIgniFlag[1]=true;
			}
		}

		//ignition 3 after cost
		if (BodyScript.effectTargetID.Count == 0 && costFlag_3)
		{
			costFlag_3 = false;
			BodyScript.Targetable.Clear ();
			
			int target = 1-player;
			int f = 3;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0 )
					BodyScript.Targetable.Add (x + 50 * target);		
			}
			
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (5);
			}
		}

		//update
		if (ManagerScript.getPhaseInt () == 7 && ManagerScript.getTurnPlayer () == player){
			turnIgniFlag[0] = false;
			turnIgniFlag[1] = false;
		}
	}
	
	void effect_1 ()
	{
		
		ManagerScript.targetableCharmIn(player,BodyScript);

		if (BodyScript.Targetable.Count > 0)
		{
			costFlag_1 = true;
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (-1);
			BodyScript.effectMotion.Add (7);	
		}
	}
	
	void effect_2 ()
	{
		ManagerScript.targetableCharmIn(player,BodyScript);

		if (BodyScript.Targetable.Count >= 2)
		{
			costFlag_2 = true;
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (-1);
			BodyScript.effectMotion.Add (7);	
			BodyScript.effectTargetID.Add (-1);
			BodyScript.effectMotion.Add (7);	
		}
		else 
			BodyScript.Targetable.Clear();
	}

	void effect_3 ()
	{
		ManagerScript.targetableCharmIn(player,BodyScript);
		
		if (BodyScript.Targetable.Count ==3)
		{
			costFlag_3 = true;
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (-1);
			BodyScript.effectMotion.Add (7);	
			BodyScript.effectTargetID.Add (-1);
			BodyScript.effectMotion.Add (7);	
			BodyScript.effectTargetID.Add (-1);
			BodyScript.effectMotion.Add (7);	
		}
		else 
			BodyScript.Targetable.Clear();
	}
}

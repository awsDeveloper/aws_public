using UnityEngine;
using System.Collections;

public class WX04_033 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool[] costFlag=new bool[1];
	
	// Use this for initialization
	void Start ()
	{
		Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;
		
		Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();

		BodyScript.powerUpValue=2000;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//always
		if (ManagerScript.getFieldInt (ID, player) == 3)
		{
			if(ManagerScript.getCostDownResetFlag(0) || field!=3){
				ManagerScript.setSpellCostDown (0, -1, 1-player, 0);
			}

			if(ManagerScript.getChantSpellID()!=-1 && ManagerScript.getChantSpellID()/50 == player)
			{
				int target=player;
				int f=3;
				int num=ManagerScript.getNumForCard(f,target);
				
				for (int i = 0; i < num; i++) {
					int x=ManagerScript.getFieldRankID(f,i,target);
					
					if(x>=0 && checkClass(x,target)){
						BodyScript.effectFlag=true;
						BodyScript.effectTargetID.Add(x+target*50);
						BodyScript.effectMotion.Add(34);
					}
				}

			}
		}
		else if(field==3){//pig
			ManagerScript.setSpellCostDown (0, 1, 1-player, 0);
		}

		//ignition
		if(BodyScript.Ignition)
		{
			BodyScript.Ignition=false;

			int target=player;
			int f=3;
			int num=ManagerScript.getNumForCard(f,target);
			
			for (int i = 0; i < num; i++) {
				int x=ManagerScript.getFieldRankID(f,i,target);
				
				if(x>=0 && checkClass(x,target) && ManagerScript.getIDConditionInt(x,target) == 1){
					BodyScript.Targetable.Add(x+50*target);
				}
			}

			if(BodyScript.Targetable.Count>=2){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(8);
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(8);

				costFlag[0]=true;
			}
			else 
				BodyScript.Targetable.Clear();
		}

		//igni after cost
		if(costFlag[0] && BodyScript.effectTargetID.Count==0)
		{
			costFlag[0]=false;

			int target = 1-player;
			int f = 3;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0 )
					BodyScript.Targetable.Add (x + 50 * target);		
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}

		}

		if (ManagerScript.getFieldInt (ID, player) == 8 && field!=8 && BodyScript.BurstFlag)
		{
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(2);

			if(getClassNum(player) > 0 ){
				int target = 1-player;
				int f = 3;
				int num = ManagerScript.getNumForCard (f, target);
				
				for (int i=0; i<num; i++)
				{
					int x = ManagerScript.getFieldRankID (f, i, target);
					if (x >= 0 )
						BodyScript.Targetable.Add (x + 50 * target);		
				}
				
				if(BodyScript.Targetable.Count>0){
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(5);
				}
			}
		}

		//update
		field = ManagerScript.getFieldInt (ID, player);
	}

	bool checkClass (int x, int cplayer)
	{
		if (x < 0)
			return false;
		int[] c = ManagerScript.getCardClass (x, cplayer);
		return (c [0] == 2 && c [1] == 3);
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


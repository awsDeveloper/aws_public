using UnityEngine;
using System.Collections;

public class WX04_037 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool bFlag=false;
	
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
		//attack trigger
		if (ManagerScript.getAttackerID() == ID + 50 * player)
		{
			BodyScript.powerUpValue=-1000*getClassNum(player);

			int target = 1-player;
			int f = 3;
			int num = ManagerScript.getNumForCard(f,target);
			
			for (int i = 0; i < num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				
				if (x >= 0)
				{
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (x+50*target);
					BodyScript.effectMotion.Add (34);
				}
			}
		}

		//signi trigger
		if(ManagerScript.getSigToTraID()!=-1 && ManagerScript.getSigToTraID()/50==1-player && ManagerScript.getFieldInt(ID,player)==3)
		{
			if(ManagerScript.getTurnPlayer() == player){
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (50*player);
				BodyScript.effectMotion.Add (26);
			}
		}

		//burst
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			BodyScript.powerUpValue=-10000;

			powerDown();

			if(BodyScript.effectFlag)
				bFlag=true;

		}

		//after burst
		if(bFlag && BodyScript.effectTargetID.Count==0)
		{
			bFlag=false;
			BodyScript.Targetable.Clear();

			if(getClassNum(player)>=1)
			{
				BodyScript.powerUpValue=-7000;

				powerDown();
			}
		}
		
		//UpDate
		field = ManagerScript.getFieldInt (ID, player);

	}

	void powerDown(){
		int target = 1 - player;
		int f = 3;
		int num = ManagerScript.getNumForCard (f, target);
		
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
			BodyScript.effectMotion.Add (34);
		}
	}
	
	bool checkClass (int x, int cplayer)
	{
		if (x < 0)
			return false;
		int[] c = ManagerScript.getCardClass (x, cplayer);
		return (c [0] == 1 && c [1] == 2);
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

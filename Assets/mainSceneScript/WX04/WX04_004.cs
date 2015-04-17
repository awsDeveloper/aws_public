using UnityEngine;
using System.Collections;

public class WX04_004 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	
	// Use this for initialization
	void Start () {
		GameObject Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		BodyScript.powerUpValue=2000;
		
		GameObject Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
		//up requirement
		if(ManagerScript.getLrigID(player)==ID && getClassNum(player)==3){
            int target = player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            //check exist in upList
            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0 && !ManagerScript.checkChanListExist(x, target, ID, player))
                {
                    //requirement add upList
                    if (true)
                        ManagerScript.alwaysChagePower(x, target, BodyScript.powerUpValue, ID, player);
                }
            }
        }
        else
            ManagerScript.powChanListChangerClear(ID, player);


		//ignition
		if(ManagerScript.getLrigID(player)==ID && BodyScript.Ignition)
		{
			BodyScript.Ignition=false;

			BodyScript.Cost[0]=0;
			BodyScript.Cost[1]=1;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=1;
			BodyScript.Cost[5]=0;

			if(ManagerScript.checkCost(ID,player))
			{
				int target = player;
				int f = 0;
				int num = ManagerScript.getNumForCard (f, target);
				
				for (int i=0; i<num; i++)
				{
					int x = ManagerScript.getFieldRankID (f, i, target);
					if (x >= 0 && checkClass(x,target))
						BodyScript.Targetable.Add (x + 50 * target);				
				}
				
				if (BodyScript.Targetable.Count > 0)
				{
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (ID + 50*player);
					BodyScript.effectMotion.Add (17);
					BodyScript.effectTargetID.Add (-2);
					BodyScript.effectMotion.Add (16);
				}			
			}
		}

		//stop attack
		if(ManagerScript.getAttackerID()!=-1 && ManagerScript.getAttackerID()/50==1-player && ManagerScript.getLrigID(player)==ID)
		{
			int rank=ManagerScript.getAttackFrontRank();
			int frontID=ManagerScript.getFieldRankID(3,rank,player);

			BodyScript.Cost[0]=1;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=1;
			BodyScript.Cost[5]=0;

			if(frontID==-1 && ManagerScript.checkCost(ID,player))
			{
				int target = player;
				int f = 2;
				int num = ManagerScript.getNumForCard (f, target);
				
				for (int i=0; i<num; i++)
				{
					int x = ManagerScript.getFieldRankID (f, i, target);
					if (x >= 0 && checkClass(x,target))
						BodyScript.Targetable.Add (x + 50 * target);				
				}
				
				if (BodyScript.Targetable.Count > 0)
				{
					BodyScript.DialogFlag=true;
				}			
			}
		}

		//receive
		if (BodyScript.messages.Count > 0)
		{
			if (BodyScript.messages [0].Contains ("Yes") && ManagerScript.checkCost(ID,player))
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (ID + 50*player);
				BodyScript.effectMotion.Add (17);
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (19);

                BodyScript.effectTargetID.Add(50*player);
                BodyScript.effectMotion.Add((int)Motions.stopAttack);
            }

			BodyScript.messages.Clear();
		}
	}
		
	bool checkClass (int x, int cplayer)
	{
		if (x < 0)
			return false;
		int[] c = ManagerScript.getCardClass (x, cplayer);
		return (c [0] == 4 && c [1] == 2);
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

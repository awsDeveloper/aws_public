using UnityEngine;
using System.Collections;

public class WX04_096 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool upFlag=false;
	
	// Use this for initialization
	void Start ()
	{
		GameObject Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;
		
		GameObject Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();

		BodyScript.powerUpValue=4000;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//always
		if(ManagerScript.getFieldInt(ID,player)==3 && ManagerScript.GetCharm(ID,player)!=-1){
            if (!upFlag)
            {
                ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower + BodyScript.powerUpValue);
                upFlag = true;
            }
        }
        else if (upFlag)
        {
            ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower);
            upFlag = false;
        }		

		//ignition
		if (BodyScript.Ignition)
		{
			BodyScript.Ignition = false;
			
			if (ManagerScript.getIDConditionInt (ID, player) == 1 )
			{
				int target=player;
				int f=3;
				int num=ManagerScript.getNumForCard(f,target);
				
				for (int i = 0; i < num; i++) {
					int x=ManagerScript.getFieldRankID(f,i,target);
					
					if(x>=0 && checkClass(x,target) && ManagerScript.GetCharm(x,target)==-1)
					{
						BodyScript.Targetable.Add(x+50*target);
					}
				}
				
				if(BodyScript.Targetable.Count>0)
				{
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (ID + 50 * player);
					BodyScript.effectMotion.Add (8);

					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(64);
				}
			}
		}

		//burst
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(2);
		}


		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}
	
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return c[0]==4 && c[1]==1;
	}
}

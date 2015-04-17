using UnityEngine;
using System.Collections;

public class PR_075 : MonoBehaviour
{
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool upFlag=false;
	int effecterID=-1;

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
		//up
		if(ManagerScript.getFieldInt(ID,player)==3 && upCheck()){
			if(!upFlag){
				ManagerScript.changeBasePower(ID,player,10000);
				upFlag=true;
			}
		}
		else if(upFlag){
			ManagerScript.changeBasePower(ID,player,7000);
			upFlag=false;
		}

		//check remove
		if(upCheck() && ManagerScript.getTargetNowID()==ID+50*player && ManagerScript.getEffecterNowID()/50==1-player)
		{
			effecterID=ManagerScript.getEffecterNowID();
		}

		//removed
		if(effecterID>=0 && !ManagerScript.getCardScr(effecterID%50,effecterID/50).effectFlag && ManagerScript.getFieldInt(ID,player)!=3)
		{
			effecterID=-1;

			int target = player;
			int f = 0;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0 && checkClass (x, target) && ManagerScript.getCardLevel(x,target)<=3)
					BodyScript.Targetable.Add (x + 50 * target);		
			}
			
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-2);
				BodyScript.effectMotion.Add (6);
			}

		}

		//burst
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			int deckNum = ManagerScript.getFieldAllNum (0, player);	
			for (int i=0; i<deckNum; i++)
			{
				int x = ManagerScript.getFieldRankID (0, i, player);
				if (x >= 0 && checkClass (x, player))
				{
					BodyScript.Targetable.Add (x + 50 * player);
				}
			}
			if (BodyScript.Targetable.Count > 0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-2);
				BodyScript.effectMotion.Add (16);
			}
		}

		//update
		field = ManagerScript.getFieldInt (ID, player);
	}
	
	bool upCheck(){
		int num=0;

		for(int i=0;i<3;i++){
			int x=ManagerScript.getFieldRankID(3,i,player);
			if(x>=0 && checkClass(x,player)){
				num++;
			}
		}
		
		return num==3;
	}

	bool checkClass (int x, int cplayer)
	{
        if (ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_アーム))
            return true;

        return ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_ウェポン);
    }
}

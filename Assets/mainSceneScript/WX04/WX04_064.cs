using UnityEngine;
using System.Collections;

public class WX04_064 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool chantFlag=false;
	int negateCount=0;
	int phase=0;

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
		//chant
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
		{
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (50*player);
			BodyScript.effectMotion.Add (22);
			BodyScript.AntiCheck=true;

			chantFlag=true;
		}

		//anti check
		if(chantFlag && BodyScript.effectTargetID.Count==0)
		{
			chantFlag=false;

			if(!BodyScript.AntiCheck){
				negateCount=2;
			}
			else 
				BodyScript.AntiCheck=false;
		}

		//negate
		if(negateCount>0 && ManagerScript.getTargetNowID()!=-1 && ManagerScript.getEffecterNowID()/50==1-player)
		{
			int tID=ManagerScript.getTargetNowID();
			int eID=ManagerScript.getEffecterNowID();
			
			int type=ManagerScript.getCardType(eID%50,eID/50);
			int f=ManagerScript.getFieldInt(tID%50,tID/50);
			
			if(tID/50==player &&( f==3 || f==4)&& type==1 )
			{
				ManagerScript.negate(eID%50,eID/50);
			}
		}


		//burst
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (50*(1-player));
			BodyScript.effectMotion.Add (59);
		}

		//decrease negateCount
		if(ManagerScript.getPhaseInt()==7 && phase!=7 && negateCount>0)
			negateCount--;

		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
		phase=ManagerScript.getPhaseInt();
	}
}

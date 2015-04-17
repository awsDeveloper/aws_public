using UnityEngine;
using System.Collections;

public class WX04_031 : MonoBehaviour
{
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool equipFlag = false;
	bool eFlg=false;
	
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
		//always
		if (field == 3 && ManagerScript.getFieldAllNum (6, player) <= 4)
		{
			if (!equipFlag && !BodyScript.DoubleCrash)
			{
				BodyScript.DoubleCrash = true;
				equipFlag = true;
			}
		} 
		else if (equipFlag)
		{
			equipFlag = false;
			BodyScript.DoubleCrash = false;
		}

		//attack trigger
		if(ManagerScript.getAttackerID() == ID+50*player)
		{
			int target=1-player;
			int f=6;
			int num=ManagerScript.getNumForCard(f,target);
			
			for (int i = 0; i < num; i++) {
				int x=ManagerScript.getFieldRankID(f,i,target);
				
				if(x>=0){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0)
			{
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(7);
				BodyScript.effectTargetID.Add(50*target);
				BodyScript.effectMotion.Add(20);
			}
		}

		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			eFlg=true;

			int target=1-player;
			int f=6;
			int num=ManagerScript.getNumForCard(f,target);
			
			for (int i = 0; i < num; i++) {
				int x=ManagerScript.getFieldRankID(f,i,target);
				
				if(x>=0 && ManagerScript.getCardScr(x,target).MultiEnaFlag){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0)
			{
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(7);
				BodyScript.effectTargetID.Add(50*target);
				BodyScript.effectMotion.Add(20);
			}
		}

		//after effect
		if(eFlg && BodyScript.effectTargetID.Count==0)
		{
			eFlg=false;

			int target=1-player;
			int f=3;
			int num=ManagerScript.getNumForCard(f,target);
			
			for (int i = 0; i < num; i++) {
				int x=ManagerScript.getFieldRankID(f,i,target);
				
				if(x>=0 && ManagerScript.getCardPower(x,target)<=8000){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0)
			{
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}
		}

		field = ManagerScript.getFieldInt (ID, player);	
	}
}

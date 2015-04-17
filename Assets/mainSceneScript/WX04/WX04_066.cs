using UnityEngine;
using System.Collections;

public class WX04_066 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	
	// Use this for initialization
	void Start ()
	{
		GameObject Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//ignition
		if (BodyScript.Ignition)
		{
			BodyScript.Ignition = false;
			
			if (ManagerScript.getIDConditionInt (ID, player) == 1)
			{
				BodyScript.Cost[0]=0;
				BodyScript.Cost[1]=0;
				BodyScript.Cost[2]=2;
				BodyScript.Cost[3]=0;
				BodyScript.Cost[4]=0;
				BodyScript.Cost[5]=0;
				
				if(ManagerScript.checkCost(ID,player)){
					
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (ID + 50 * player);
					BodyScript.effectMotion.Add (17);
					BodyScript.effectTargetID.Add (ID + 50 * player);
					BodyScript.effectMotion.Add (65);

					int target=1-player;
					for(int i=0;i<3;i++){
						int x=ManagerScript.getFieldRankID(3,i,target);
						if(x>=0 && ManagerScript.getCardPower(x,target)<=8000){
							BodyScript.Targetable.Add(x+50*(target));
						}
					}
					if(BodyScript.Targetable.Count>0){
						BodyScript.effectFlag=true;
						BodyScript.effectTargetID.Add(-1);
						BodyScript.effectMotion.Add(5);
					}
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
}

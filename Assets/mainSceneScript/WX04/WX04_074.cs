using UnityEngine;
using System.Collections;

public class WX04_074 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool cFlag=false;
	
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
			yaki(5000);
			cFlag=true;
		}

		if(cFlag && BodyScript.effectTargetID.Count==0)
		{
			cFlag=false;

			yaki(10000);
		}

		//burst
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			yaki(5000);

			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(26);
		}
		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}

	void yaki(int p){
		int target=1-player;
		for(int i=0;i<3;i++){
			int x=ManagerScript.getFieldRankID(3,i,target);
			if(x>=0 && ManagerScript.getCardPower(x,target)<=p){
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

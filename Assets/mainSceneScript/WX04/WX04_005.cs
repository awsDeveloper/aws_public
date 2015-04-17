using UnityEngine;
using System.Collections;

public class WX04_005 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool cipFlag=false;
	
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
		BodyScript.useLimit = ManagerScript.getFieldAllNum (5, player) > 1;
		

		//always
		if(ID == ManagerScript.getLrigID(player))
		{
			ManagerScript.GrowPhaseSkip[player]=true;

			ManagerScript.normalDrawNum[0]=1;
			ManagerScript.normalDrawNum[1]=1;

			ManagerScript.signiSumLimit[0] = 1;
			ManagerScript.signiSumLimit[1] = 1;
		}

		//cip
		if (ManagerScript.getFieldInt (ID, player) == 4 && field != 4)
		{
			effect(player);
			cipFlag=true;
		}

		//after cip
		if(cipFlag && BodyScript.effectTargetID.Count==0){
			cipFlag=false;
			BodyScript.Targetable.Clear();

			effect(1-player);
		}

		field = ManagerScript.getFieldInt (ID, player);	
	}

	void effect(int target){
		int f = 3;
		int num = ManagerScript.getFieldAllNum (f, target);
		if (f == 3)
			num = 3;
		
		for (int i=0; i<num; i++)
		{
			int x = ManagerScript.getFieldRankID (f, i, target);
			if (x >= 0)
			{
				BodyScript.Targetable.Add (x + 50 * target);
			}
		}
		
		if (BodyScript.Targetable.Count >= 2)
		{
			BodyScript.effectSelecter = target;
			for(int i=0;i<BodyScript.Targetable.Count-1;i++){
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (7);
			}
		}
		else BodyScript.Targetable.Clear();
	}
}

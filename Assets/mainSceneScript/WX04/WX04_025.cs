using UnityEngine;
using System.Collections;

public class WX04_025 : MonoBehaviour {
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

		BodyScript.attackArts=true;
		BodyScript.notMainArts=true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//cip
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8)
		{
			effect(player);
			cipFlag=true;
		}
		
		//after cip
		if(cipFlag && BodyScript.effectTargetID.Count==0){
			cipFlag=false;
			BodyScript.Targetable.Clear();
			
			effect(1-player);

			if(ManagerScript.getFieldAllNum(3,player)==0){
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (50*player);
				BodyScript.effectMotion.Add (41);
			}
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
		
		if (BodyScript.Targetable.Count>0)
		{
			BodyScript.effectSelecter = target;
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (-1);
			BodyScript.effectMotion.Add (7);
		}
	}
}

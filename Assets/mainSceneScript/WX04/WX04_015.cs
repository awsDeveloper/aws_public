using UnityEngine;
using System.Collections;

public class WX04_015 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	
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
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
		{
			int target = 1-player;
			int f = 0;
			int num = ManagerScript.getNumForCard (f, target);
			
			bool flag=false;
			
			for (int i=num-1; i>=0; i--)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0){
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (x+50*target);
					BodyScript.effectMotion.Add (54);
					
					if(ManagerScript.getCardType(x,target)==3)
					{
						flag=true;
						break;
					}
				}
			}
			
			int c=BodyScript.effectTargetID.Count;
			
			if(flag)
			{
				BodyScript.effectTargetID.Add (BodyScript.effectTargetID[c-1] );
				BodyScript.effectMotion.Add (68);
			}
			
			if(c>1){
				for (int i = 0; i < c-1; i++) {
					BodyScript.effectTargetID.Add (BodyScript.effectTargetID[i] );
					BodyScript.effectMotion.Add (48);
				}
				
				BodyScript.effectTargetID.Add (50*target);
				BodyScript.effectMotion.Add (24);
			}
		}
		field = ManagerScript.getFieldInt (ID, player);
	}
}

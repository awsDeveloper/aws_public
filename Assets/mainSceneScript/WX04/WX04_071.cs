using UnityEngine;
using System.Collections;

public class WX04_071 : MonoBehaviour {

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
		//ignition
		if (BodyScript.Ignition)
		{
			BodyScript.Ignition = false;
			
			if (ManagerScript.getIDConditionInt (ID, player) == 1)
			{
				BodyScript.Cost[0]=0;
				BodyScript.Cost[1]=0;
				BodyScript.Cost[2]=1;
				BodyScript.Cost[3]=0;
				BodyScript.Cost[4]=0;
				BodyScript.Cost[5]=0;
				
				if(ManagerScript.checkCost(ID,player)){
					
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (ID + 50 * player);
					BodyScript.effectMotion.Add (17);
					BodyScript.effectTargetID.Add (ID + 50 * player);
					BodyScript.effectMotion.Add (65);
					
					int target=player;
					int f=0;
					int num=ManagerScript.getNumForCard(f,target);

					for(int i=0;i<num;i++){
						int x=ManagerScript.getFieldRankID(f,i,target);
						if(x>=0 && ManagerScript.getSpellColor(x,target)==2 && ManagerScript.getCostSum(x,target)==1){
							BodyScript.Targetable.Add(x+50*(target));
						}
					}
					if(BodyScript.Targetable.Count>0){
						BodyScript.effectFlag=true;
						BodyScript.effectTargetID.Add(-2);
						BodyScript.effectMotion.Add(16);
					}
				}
			}
		}	
		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}
}

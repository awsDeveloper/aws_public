using UnityEngine;
using System.Collections;

public class WX04_093 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	int chantCount=0;
	bool antiCheckUp=false;
	
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
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag)
		{
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(ID+50*player);
			BodyScript.effectMotion.Add(22);
		
			antiCheckUp=true;
			BodyScript.AntiCheck=true;
		}

		if(antiCheckUp && BodyScript.effectTargetID.Count==0)
		{
			antiCheckUp=false;

			if(!BodyScript.AntiCheck)
			{
				chantCount=1;
			}
			else
				BodyScript.AntiCheck=false;
		}

		//after chant
		if(chantCount>0 && BodyScript.effectTargetID.Count==0)
		{
			chantCount = (chantCount+1)%4;

			int target = player;
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
					
					if(ManagerScript.getCardType(x,target)==2)
					{
						flag=true;
						break;
					}
				}
			}
			
			int c=BodyScript.effectTargetID.Count;
			
			if(flag)
			{
				int tID=BodyScript.effectTargetID[c-1];
				BodyScript.effectTargetID.Add (tID);

				if(!ManagerScript.summonLim(tID%50,tID/50))
					BodyScript.effectMotion.Add (6);
				else 
					BodyScript.effectMotion.Add (7);
			}
			
			if(c>1){
				for (int i = 0; i < c-1; i++) {
					BodyScript.effectTargetID.Add (BodyScript.effectTargetID[i] );
					BodyScript.effectMotion.Add (7);
				}
			}
		}

		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag)
		{
			int target = player;
			int f = 0;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=num-1; i>num-4; i--)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0){
					BodyScript.Targetable.Add(x+50*target);
				}
			}

			if(BodyScript.Targetable.Count>0)
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-2);
				BodyScript.effectMotion.Add (16);
				BodyScript.TargetIDEnable=true;
			}
		}

		if(BodyScript.TargetID.Count>0)
		{
			int tID=BodyScript.TargetID[0];
			BodyScript.TargetID.Clear();
			BodyScript.TargetIDEnable=false;

			for (int i = 0; i < BodyScript.Targetable.Count; i++)
			{
				BodyScript.effectTargetID.Add (BodyScript.Targetable[i] );
				BodyScript.effectMotion.Add (7);
			}

		}

		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}
}

using UnityEngine;
using System.Collections;

public class WX04_052 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	int cID=-1;
	bool bFlag=false;

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
		//cip
		if (ManagerScript.getFieldInt (ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
		{
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(ID+50*player);
			BodyScript.effectMotion.Add(64);
		}

		//burst
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			int target=player;
			int f=0;
			int num=ManagerScript.getNumForCard(f,target);
			
			for (int i = num-1; i > num-4; i--) {
				int x=ManagerScript.getFieldRankID(f,i,target);
				
				if(x>=0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(7);

					bFlag=true;
				}
			}
		}

		if(bFlag && BodyScript.effectTargetID.Count==0)
		{
			bFlag=false;

			int target=player;
			int f=7;
			int num=ManagerScript.getNumForCard(f,target);
			
			for (int i = num-1; i > num-4; i--) {
				int x=ManagerScript.getFieldRankID(f,i,target);
				
				if(x>=0 && checkClass(x,target))
				{
					BodyScript.Targetable.Add(x+50*target);
				}
			}

			if(BodyScript.Targetable.Count>0)
			{
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
		}

		//Replace
		if(ManagerScript.getFieldInt (ID, player) == 3 && ManagerScript.getBanishingID()!=-1)
		{
			int tID=ManagerScript.getBanishingID();

			if(checkClass(tID%50,tID/50) && tID/50==player && (cID = ManagerScript.GetCharm(tID%50,tID/50))!=-1)
			{
				cID+=player*50;
				BodyScript.DialogFlag=true;
				BodyScript.ReplaceFlag=true;
			}
		}

		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes"))
			{
				BodyScript.effectTargetID.Add(cID);
				BodyScript.effectMotion.Add(7);

				ManagerScript.Replace(BodyScript);
			}
			
			BodyScript.messages.Clear();
		}		

		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}

	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return c[0]==4 && c[1]==1;
	}
}

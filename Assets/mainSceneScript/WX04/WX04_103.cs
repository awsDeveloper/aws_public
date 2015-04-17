using UnityEngine;
using System.Collections;

public class WX04_103 : MonoBehaviour {
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
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
		{
			int target= 1- player;
			int f=3;
			int num=ManagerScript.getNumForCard(f,target);
			
			for (int i = 0; i < num; i++) {
				int x=ManagerScript.getFieldRankID(f,i,target);
				
				if(x>=0)
				{
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0)
			{
				cFlag=true;

				BodyScript.powerUpValue=-1000*classLevelSum(player);

				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(34);
			}
		}

		if(cFlag && BodyScript.effectTargetID.Count==0)
		{
			cFlag=false;

			int target= player;
			int f=3;
			int num=ManagerScript.getNumForCard(f,target);
			
			for (int i = 0; i < num; i++) {
				int x=ManagerScript.getFieldRankID(f,i,target);
				
				if(x>=0 && checkClass(x,target) && ManagerScript.GetCharm(x,target)==-1)
				{
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0)
			{
				BodyScript.CharmizeID=ID+50*player;

				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(63);
			}
		}

		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}
	
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return c[0]==4 && c[1]==1;
	}

	int classLevelSum(int target){
		int sum=0;

		for (int i = 0; i < 3; i++)
		{
			int x=ManagerScript.getFieldRankID(3,i,target);

			if(checkClass(x,target))
			{
				sum+=ManagerScript.getCardLevel(x,target);
			}
		}

		return sum;
	}
}

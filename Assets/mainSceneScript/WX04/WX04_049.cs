using UnityEngine;
using System.Collections;

public class WX04_049 : MonoBehaviour {
	
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;

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

			BodyScript.Cost[0]=0;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=2;
			BodyScript.Cost[5]=0;

			if (ManagerScript.getIDConditionInt (ID, player) == 1 && ManagerScript.checkCost(ID,player) )
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (ID + 50 * player);
				BodyScript.effectMotion.Add (17);

				int target=player;
				int f=3;
				int num=ManagerScript.getNumForCard(f,target);

				for (int i = 0; i < num; i++) {
					int x=ManagerScript.getFieldRankID(f,i,target);

					if(x>=0)
					{
						BodyScript.effectTargetID.Add(x+50*target);
						BodyScript.effectMotion.Add(61);
					}
				}
			}
		}

		//always
		if(ManagerScript.getFieldInt(ID,player)==3 && getClassNum(player)>0)
		{
			BodyScript.Level=2;
		}
		else 
			BodyScript.Level=3;
	}

	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return (c[0]==5 && c[1]==1)||(c[0]==5 && c[1]==2);
	}

	int getClassNum(int target){//自分以外
		int num=0;
		for (int i = 0; i < 3; i++)
		{
			int x=ManagerScript.getFieldRankID(3,i,target);
			if(x>=0 && checkClass(x,target) && x+target*50 != ID + 50*player)
				num++;
		}
		return num;
	}
}

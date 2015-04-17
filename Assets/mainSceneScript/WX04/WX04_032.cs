using UnityEngine;
using System.Collections;

public class WX04_032 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool bFlag=false;
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
		BodyScript.Cost[2]=5-getClassNum(player);
		
		//chant
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
		{
			SigniBanish();

			if(BodyScript.effectFlag)
				cFlag=true;				
		}

		//after chant
		if(cFlag && BodyScript.effectTargetID.Count==0)
		{
			cFlag=false;
			EnaTrash();
		}
		
		//burst
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			EnaTrash();

			if(BodyScript.effectFlag || ManagerScript.getFieldAllNum(6,1-player)<=4)
				bFlag=true;
		}

		//after burst
		if(bFlag && BodyScript.effectTargetID.Count==0 && ManagerScript.getFieldAllNum(6,1-player)<=4)
		{

			bFlag=false;
			SigniBanish();
		}
		
		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}

	void EnaTrash(){
		int target=1-player;
		int f=6;
		int num=ManagerScript.getNumForCard(f,target);
		
		for (int i = 0; i < num; i++) {
			int x=ManagerScript.getFieldRankID(f,i,target);
			
			if(x>=0){
				BodyScript.Targetable.Add(x+50*target);
			}
		}
		
		if(BodyScript.Targetable.Count>0)
		{
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(-1);
			BodyScript.effectMotion.Add(7);
			BodyScript.effectTargetID.Add(50*target);
			BodyScript.effectMotion.Add(20);
		}
	}

	void SigniBanish(){
		int target=1-player;
		int f=3;
		int num=ManagerScript.getNumForCard(f,target);
		
		for (int i = 0; i < num; i++) {
			int x=ManagerScript.getFieldRankID(f,i,target);
			
			if(x>=0 && ManagerScript.getCardPower(x,target)<=10000){
				BodyScript.Targetable.Add(x+50*target);
			}
		}
		
		if(BodyScript.Targetable.Count>0)
		{
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(-1);
			BodyScript.effectMotion.Add(5);
		}
	}
	
	bool checkClass (int x, int cplayer)
	{
		if (x < 0)
			return false;
		int[] c = ManagerScript.getCardClass (x, cplayer);
		return (c [0] == 5 && c [1] == 3);
	}
	
	int getClassNum(int target){
		int num=0;
		for (int i = 0; i < 3; i++)
		{
			int x=ManagerScript.getFieldRankID(3,i,target);
			if(x>=0 && checkClass(x,target))
				num++;
		}
		return num;
	}
}

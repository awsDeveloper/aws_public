using UnityEngine;
using System.Collections;

public class SP02_010_eq : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool equipFlag = false;
	
	// Use this for initialization
	void Start ()
	{
		GameObject Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;
		
		GameObject Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//check equip
		if (/*ManagerScript.getFieldInt (ID, player) == 3 &&*/ checkEquip())
		{
			if (!equipFlag && !BodyScript.Assassin)
			{
				equipFlag = true;
				BodyScript.Assassin = true;
			}
		} 
		else
		{
			dequip();
		}

		//自爆
		if(ManagerScript.getPhaseInt()==7)
		{
			dequip();
			Destroy(this);
		}
	}

	bool checkEquip(){
		int rank=ManagerScript.getRank(ID,player);
		rank=1-(rank-1);//正面のランクを得る

		int x=ManagerScript.getFieldRankID(3,rank,1-player);

		return x>=0 && ManagerScript.getCardPower(x,1-player)>=12000;
	}

	void dequip()
	{
		if (equipFlag)
		{
			equipFlag = false;
			BodyScript.Assassin = false;
		}
	}
}

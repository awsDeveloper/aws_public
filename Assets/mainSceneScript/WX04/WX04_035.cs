using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX04_035 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool cFlag=false;

	List<int> equipList = new List<int> ();
	bool firstChecked = false;

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
		//checkEquip
		checkEquip ();

		//cip
		if (ManagerScript.getFieldInt (ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
		{
			ManagerScript.requipUpFlag=true;
		}

		//negate
		if(field==3 && ManagerScript.getTargetNowID()!=-1 && ManagerScript.getEffecterNowID()/50==1-player)
		{
			int tID=ManagerScript.getTargetNowID();
			int eID=ManagerScript.getEffecterNowID();

			int type=ManagerScript.getCardType(eID%50,eID/50);
			int f=ManagerScript.getFieldInt(tID%50,tID/50);

			if(tID/50==player && f==3 && checkClass(tID%50,tID/50) && (type==0||type==2) )
			{
				ManagerScript.negate(eID%50,eID/50);
			}
		}


		//moyasi
		if (ManagerScript.getEffectGoTrashID()== ID+ player*50 && ManagerScript.getEffecterNowID()/50==1-player)
		{
			BodyScript.Cost[0]=0;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=1;
			BodyScript.Cost[5]=0;

			if(ManagerScript.checkCost(ID,player))
			{
				BodyScript.DialogFlag=true;
			}
		}
		
		//burst
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
		{
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (50*player);
			BodyScript.effectMotion.Add (26);

			cFlag=true;
		}
		
		//receive
		if (BodyScript.messages.Count > 0)
		{
			if (BodyScript.messages [0].Contains ("Yes") && ManagerScript.checkCost(ID,player))
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (ID+50*player);
				BodyScript.effectMotion.Add (17);
				BodyScript.effectTargetID.Add (ID+50*player);
				BodyScript.effectMotion.Add (16);
			}

			BodyScript.messages.Clear ();
		}
		
		if(cFlag && BodyScript.effectTargetID.Count==0)
		{
			cFlag=false;

			if(getClassNum(player)>=5){
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (50*player);
				BodyScript.effectMotion.Add (41);
			}
		}
		
		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}
	
	bool checkClass (int x, int cplayer)
	{
		if (x < 0)
			return false;
		int[] c = ManagerScript.getCardClass (x, cplayer);
		return (c [0] == 4 && c [1] == 2);
	}
	
	int getClassNum(int target){//ver. ena
		int num=0;

		for (int i = 0; i < ManagerScript.getFieldAllNum(6,target); i++)
		{
			int x=ManagerScript.getFieldRankID(6,i,target);
			if(x>=0 && checkClass(x,target))
				num++;
		}
		return num;
	}

	//equip
	void equip (int x, int eplayer)
	{
		if (x < 0)
			return;
		
		if( ManagerScript.alwaysChangeResiLrig(x, eplayer, true, ID, player) )
			equipList.Add (x + 50 * eplayer);

	}

	bool checkTarget (int x, int eplayer)
	{
		CardScript sc = ManagerScript.getCardScr (x, eplayer);

		return ManagerScript.getFieldInt (x, eplayer) == 3 && checkClass(x,eplayer);
	}

	bool isAfterEquip (int x, int eplayer)
	{
		CardScript sc = ManagerScript.getCardScr (x, eplayer);
		return  sc.resiLrigEffect == true;
	}

	void dequip (int index)
	{
		if (index >= equipList.Count)
			return;
		
		int px = equipList [index];
		int x = px % 50;
		int eplayer = px / 50;
		
		if(ManagerScript.alwaysChangeResiLrig(x, eplayer, false, ID, player))
			equipList.RemoveAt (index);
	}

	void checkEquip ()
	{
		//requip check
		if (ManagerScript.requipFlag)
			equipClear ();
		
		//check situation
		if (ManagerScript.getFieldInt(ID, player) == 3)
		{
			int target = player;
			int f=3;
			int num=ManagerScript.getNumForCard(f,target);

			//場のカード対象
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID(f,i,target);

				//checkTarget
				if (x>=0 && checkTarget (x, target) && !isAfterEquip (x, target) && !checkExist (x, target))
					equip (x, target);				
			}
			
			//firstChecked
			firstChecked = true;
		}
		else
		{
			equipClear ();
		}
		
		//equip target check
		if (equipList.Count > 0)
		{
			for (int i=0; i<equipList.Count; i++)
			{
				int x = equipList [i] % 50;
				int target = equipList [i] / 50;
				
				if (!checkTarget (x, target))
				{
					dequip (i);
					i--;
				}				
			}
		}
	}

	void equipClear ()
	{
		while (equipList.Count>0)
		{
			dequip (0);
		}
		
		firstChecked = false;
	}

	bool checkExist (int x, int player)
	{
		if (!firstChecked)
			return false;
		
		for (int i=0; i<equipList.Count; i++)
		{
			if (x + 50 * player == equipList [i])
				return true;
		}
		
		return false;
	}
}

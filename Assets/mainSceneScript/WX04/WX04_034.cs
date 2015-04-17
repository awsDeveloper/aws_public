using UnityEngine;
using System.Collections;

public class WX04_034 : MonoBehaviour
{
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	int count = 1;
	bool inputed=false;
	bool antiUp=false;
	int costNum=0;
	
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
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (ID + 50 * player);
			BodyScript.effectMotion.Add (22);
			BodyScript.AntiCheck=true;
			antiUp=true;

			if(!inputed){
				BodyScript.DialogFlag = true;
				BodyScript.DialogNum = 1;
				BodyScript.DialogCountMax = getClassNameNum (player) / 2;
				BodyScript.DialogStr = "バニッシュする数";
				BodyScript.DialogStrEnable = true;
			}
		}
		
		//receive
		if (BodyScript.messages.Count > 0)
		{
			count = int.Parse (BodyScript.messages [0]);
			BodyScript.messages.Clear ();

			costNum = count*2;

			inputed=true;
		}

		if(inputed && antiUp && BodyScript.effectTargetID.Count==0)
		{
			antiUp=false;
			inputed=false;

			if(!BodyScript.AntiCheck)
			{
				targetIn();

				if(BodyScript.Targetable.Count>0)
				{
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(19);

					BodyScript.TargetIDEnable=true;
				}
			}
			else
				BodyScript.AntiCheck=false;
		}

		if(BodyScript.TargetID.Count>0)
		{
			int tID=BodyScript.TargetID[0];
			BodyScript.TargetID.Clear();

			string name=ManagerScript.getCardScr(tID%50,tID/50).Name;

			costNum--;

			if(costNum>0)
			{
				targetIn();

				for (int i = 0; i < BodyScript.Targetable.Count; i++) {

					int x=BodyScript.Targetable[i];

					if(name==ManagerScript.getCardScr(x%50,x/50).Name)
					{
						BodyScript.Targetable.RemoveAt(i);
						i--;
					}
				}

				if(BodyScript.Targetable.Count>0)
				{
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(19);
				}
			}
			else
			{
				BodyScript.Targetable.Clear();
				BodyScript.TargetIDEnable=false;

				int target = 1 - player;
				int f = 3;
				int num=ManagerScript.getNumForCard(f,target);
				
				for (int i = 0; i < num; i++) {
					int x=ManagerScript.getFieldRankID(f,i,target);
					
					if(x>=0)
						BodyScript.Targetable.Add(x+50*target);
				}

				for (int i = 0; i < BodyScript.Targetable.Count && i < count; i++) {
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(5);	
				}
			}
		}

		field = ManagerScript.getFieldInt (ID, player);	
	}

	bool checkClass (int x, int cplayer)
	{
		if (x < 0)
			return false;
		int[] c = ManagerScript.getCardClass (x, cplayer);
		return (c [0] == 2 && c [1] == 3);
	}
	
	int getClassNameNum (int target)
	{
		int num = 0;

		int f = 2;
		int max = ManagerScript.getNumForCard (f, target);

		for (int i = 0; i < max; i++)
		{
			int x = ManagerScript.getFieldRankID (f, i, target);
			if (x >= 0 && checkClass (x, target))
			{

				string name = ManagerScript.getCardScr (x, target).Name;
				bool flag = false;

				for (int j = i + 1; j < max; j++)
				{
					int y = ManagerScript.getFieldRankID (f, j, target);

					if (name == ManagerScript.getCardScr (y, target).Name)
						flag = true;
				}

				if (!flag)
					num++;
			}
		}
		return num;
	}

	void targetIn()
	{
		int target=player;
		int f=2;
		int num=ManagerScript.getNumForCard(f,target);
		
		for (int i = 0; i < num; i++) {
			int x=ManagerScript.getFieldRankID(f,i,target);
			
			if(x>=0 && checkClass(x,target))
				BodyScript.Targetable.Add(x+50*target);
		}

	}
}
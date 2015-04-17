using UnityEngine;
using System.Collections;

public class SP01_010 : MonoBehaviour
{
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	int count=0;
	bool existFlag=false;
	bool bounceFlag=false;

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
		if (ManagerScript.getFieldInt (ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
		{
			BodyScript.DialogFlag=true;
			BodyScript.DialogNum=1;
			BodyScript.DialogCountMax=4;
			BodyScript.DialogStr="宣言するレベル";
			BodyScript.DialogStrEnable=true;
		}

		//receive
		if(BodyScript.messages.Count>0){
			count=int.Parse(BodyScript.messages[0]);
			BodyScript.messages.Clear();

			int target = player;
			int f = 0;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=num-1; i>num-4; i--)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0){

					if(ManagerScript.getCardLevel(x,target)==count)
						existFlag=true;

					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (x+50*target);
					BodyScript.effectMotion.Add (54);
				}
			}

			if(!existFlag && BodyScript.effectTargetID.Count>0)
				bounceFlag=true;
		}

		//after exist
		if(existFlag && BodyScript.effectTargetID.Count==0)
		{
			existFlag=false;
			BodyScript.Targetable.Clear();
			bounceFlag=true;

			//バウンス
			int target = 1 - player;
			int f = 3;
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
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (16);
			}
		}

		//after bounce
		if(bounceFlag && BodyScript.effectTargetID.Count==0)
		{

			BodyScript.Targetable.Clear();
			bounceFlag=false;

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
				for (int i = 0; i < BodyScript.Targetable.Count; i++) {
					BodyScript.effectFlag = true;
					BodyScript.effectTargetID.Add (-2);
					BodyScript.effectMotion.Add (25);
				}
			}
		}

		//update
		field = ManagerScript.getFieldInt (ID, player);
	}
}

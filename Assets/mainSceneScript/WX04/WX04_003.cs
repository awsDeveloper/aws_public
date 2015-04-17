using UnityEngine;
using System.Collections;

public class WX04_003 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	bool[] costFlag=new bool[2];

	// Use this for initialization
	void Start ()
	{
		GameObject Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;

		GameObject Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();

		BodyScript.SpellCutIn=true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//cip
		if (ManagerScript.getCipID () == ID + 50 * player)
		{
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*(1-player));
			BodyScript.effectMotion.Add(66);

			int target=1-player;
			int f=2;
			int num=ManagerScript.getNumForCard(f,target);

			for (int i = 0; i < num; i++) {
				int x=ManagerScript.getFieldRankID(f,i,target);

				if(x>=0 && ManagerScript.getCardType(x,target)==3){
					BodyScript.Targetable.Add(x+50*target);
				}
			}

			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(68);
			}

			BodyScript.effectTargetID.Add(50*(1-player));
			BodyScript.effectMotion.Add(43);

			BodyScript.effectTargetID.Add(50*(1-player));
			BodyScript.effectMotion.Add(67);
		}

		//anti spell
		if(BodyScript.UseSpellCutIn)
		{
			BodyScript.UseSpellCutIn=false;

			BodyScript.Cost[0]=0;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=2;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=0;

			if(ManagerScript.checkCost(ID,player)){
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (ID + 50 * player);
				BodyScript.effectMotion.Add (17);

				BodyScript.effectTargetID.Add (ID + 50 * player);
				BodyScript.effectMotion.Add (52);
			}
		}

		if(BodyScript.Ignition)
		{
			BodyScript.Ignition=false;

			BodyScript.changeColorCost(3,1);

			if(ManagerScript.checkCost(ID,player)){

				targetIn_2();

				if(BodyScript.Targetable.Count>0){

					BodyScript.Targetable.Clear();

					targetIn_1();

					if(BodyScript.Targetable.Count>0){
						costFlag[0]=true;

						BodyScript.effectFlag=true;
						BodyScript.effectTargetID.Add(ID+50*player);
						BodyScript.effectMotion.Add(17);

						BodyScript.effectTargetID.Add(-1);
						BodyScript.effectMotion.Add(19);
					}
				}
			}
		}

		if(costFlag[0] && BodyScript.effectTargetID.Count==0)
		{
			costFlag[0]=false;

			targetIn_2();

			if(BodyScript.Targetable.Count>0){
				costFlag[1]=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
			}
		}

		if(costFlag[1] && BodyScript.effectTargetID.Count==0){
			costFlag[1]=false;

			int target = 1-player;
			int f = 3;
			int num = ManagerScript.getNumForCard (f, target);
			
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0 )
					BodyScript.Targetable.Add (x + 50 * target);		
			}

			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}

		}
		
	}

	void targetIn_1(){
		int target = player;
		int f = 2;
		int num = ManagerScript.getNumForCard (f, target);
		
		for (int i=0; i<num; i++)
		{
			int x = ManagerScript.getFieldRankID (f, i, target);
			if (x >= 0 && ManagerScript.getCardType(x,target)==3)
				BodyScript.Targetable.Add (x + 50 * target);		
		}
	}

	void targetIn_2(){
		int target = player;
		int f = 2;
		int num = ManagerScript.getNumForCard (f, target);
		
		for (int i=0; i<num; i++)
		{
			int x = ManagerScript.getFieldRankID (f, i, target);
			if (x >= 0 && checkClass (x, target))
				BodyScript.Targetable.Add (x + 50 * target);		
		}
	}

	bool checkClass (int x, int cplayer)
	{
		if (x < 0)
			return false;
		int[] c = ManagerScript.getCardClass (x, cplayer);
		return (c [0] == 2 && c [1] == 3);
	}
}

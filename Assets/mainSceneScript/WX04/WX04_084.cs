using UnityEngine;
using System.Collections;

public class WX04_084 : MonoBehaviour {
	
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;

	bool[] cFlag=new bool[2];
	
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
		if (ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag )
		{
			searchSpell(1);
			cFlag[0]=true;
		}

		//after chant
		for (int i = 0; i < cFlag.Length; i++)
		{
			if(cFlag[i] && BodyScript.effectTargetID.Count==0)
			{
				cFlag[i]=false;
				BodyScript.Targetable.Clear();

				searchSpell(i+2);

				if(i==0)
					cFlag[1]=true;
			}
		}

		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}

	void searchSpell(int costSum){
		int target=player;
		int f=0;
		int num=ManagerScript.getNumForCard(f,target);
		
		for(int i=0;i<num;i++){
			int x=ManagerScript.getFieldRankID(f,i,target);
			if(x>=0 && ManagerScript.getCostSum(x,target)==costSum){
				BodyScript.Targetable.Add(x+50*(target));
			}
		}
		if(BodyScript.Targetable.Count>0){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(-2);
			BodyScript.effectMotion.Add(16);
		}
	}
}

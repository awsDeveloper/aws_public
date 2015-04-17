using UnityEngine;
using System.Collections;

public class WX05_017 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	
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
	void Update () {
		//chant
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int target = player;
			int f=3;
			int num=ManagerScript.getNumForCard(f,target);

			for (int i = 0; i < num; i++) {
				int x=ManagerScript.getFieldRankID(f,i,target);

				if(x>=0)
					BodyScript.Targetable.Add(x+50*target);
			}

			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(70);
			}
		}
		
		//UpDate
		field=ManagerScript.getFieldInt(ID,player);
	}
}

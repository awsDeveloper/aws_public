using UnityEngine;
using System.Collections;

public class PR_071 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	bool equipFlag=false;

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
	void Update () {

		//check equip
		if(ManagerScript.getFieldInt(ID,player)==3 && ManagerScript.getChantSpellID()!=-1)
		{
			int cID=ManagerScript.getChantSpellID();

			if(ManagerScript.getCardColor(cID%50,cID/50)==2 && cID/50 == player)
			{
				equipFlag=true;
			}
		}

		//equip
		if(equipFlag && !BodyScript.DoubleCrash){
			BodyScript.DoubleCrash=true;
		}

		//dequip
		if(equipFlag &&( ManagerScript.getPhaseInt()==7 || ManagerScript.getFieldInt(ID,player)!=3))
		{
			equipFlag=false;
			BodyScript.DoubleCrash=false;
		}

	
	}
}

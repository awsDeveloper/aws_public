using UnityEngine;
using System.Collections;

public class WX05_014 : MonoBehaviour {
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

		BodyScript.attackArts=true;
		BodyScript.SpellCutIn=true;
		BodyScript.notMainArts=true;
	}
	
	// Update is called once per frame
	void Update () {
		//chant
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(69);
		}
		
		//UpDate
		field=ManagerScript.getFieldInt(ID,player);
	}
}

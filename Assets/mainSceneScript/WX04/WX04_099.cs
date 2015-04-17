using UnityEngine;
using System.Collections;

public class WX04_099 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;

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
	void Update () {
		if(ManagerScript.getBattleFinishID(player)==ID && ManagerScript.getTurnPlayer()==1-player)
		{
			int bID=ManagerScript.getBattleFinishID(1-player);

			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(bID+50*(1-player));
			BodyScript.effectMotion.Add(5);
		}
	}
}

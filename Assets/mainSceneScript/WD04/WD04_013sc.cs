using UnityEngine;
using System.Collections;

public class WD04_013sc : MonoBehaviour {
	GameObject Body;
	CardScript BodyScript;
	int player=-1;
	bool didflag=false;

	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		player=BodyScript.player;		
	}
	
	// Update is called once per frame
	void Update () {
		if(BodyScript.Power>=5000 && BodyScript.AttackNow && !didflag){
			didflag=true;
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(26);
		}
		if(!BodyScript.AttackNow)didflag=false;
	}
}

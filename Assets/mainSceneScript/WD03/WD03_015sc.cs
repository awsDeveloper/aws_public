using UnityEngine;
using System.Collections;

public class WD03_015sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;

	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int handNum=ManagerScript.getFieldAllNum(2,1-player);
			if(handNum>0){
				BodyScript.effectFlag=true;
//				int rand=Random.Range(0,handNum);
//				BodyScript.effectTargetID.Add(ManagerScript.getFieldRankID(2,rand,1-player)+50*(1-player));
//				BodyScript.effectMotion.Add(19);
				BodyScript.effectTargetID.Add(50*(1-player));
				BodyScript.effectMotion.Add(32);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

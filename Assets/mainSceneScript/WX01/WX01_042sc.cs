using UnityEngine;
using System.Collections;

public class WX01_042sc : MonoBehaviour {
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
			int num2=ManagerScript.getFieldAllNum(5,1-player);
			if(num2>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ManagerScript.getFieldRankID(5,num2-1,1-player)+(1-player)*50);
				BodyScript.effectMotion.Add(28);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);
	}
}

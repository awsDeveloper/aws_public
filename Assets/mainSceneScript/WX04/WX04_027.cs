using UnityEngine;
using System.Collections;

public class WX04_027 : MonoBehaviour {
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
		BodyScript.SpellCutIn=true;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.attackArts=true;
		BodyScript.powerUpValue=3000;
	}
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			for(int i=0;i<6;i++){
				int x=ManagerScript.getFieldRankID(3,i%3,i/3);
				if(x>0){
					BodyScript.Targetable.Add(x+50*(i/3));
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(34);
			}
		}

		field=ManagerScript.getFieldInt(ID,player);	
	}
}

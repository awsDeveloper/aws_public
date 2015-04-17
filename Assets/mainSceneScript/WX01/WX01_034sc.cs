using UnityEngine;
using System.Collections;

public class WX01_034sc : MonoBehaviour {
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
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8){
			int deckNum=ManagerScript.getFieldAllNum(0,player);	
			if(deckNum>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ManagerScript.getFieldRankID(0,deckNum-1,player)+50*player);
				BodyScript.effectMotion.Add(3);
				int allEnaNum=0;
				for(int i=0;i<6;i++)allEnaNum+=ManagerScript.getEnaColorNum(i,player);
				if(!BodyScript.BurstFlag && deckNum>=2 && allEnaNum>=10){
					BodyScript.effectTargetID.Add(ManagerScript.getFieldRankID(0,deckNum-2,player)+50*player);
					BodyScript.effectMotion.Add(3);
				}
			}
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

using UnityEngine;
using System.Collections;

public class WD01_007sc : MonoBehaviour {
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
			int deckNum=ManagerScript.getFieldAllNum(0,player);			
			for(int i=0;i<deckNum;i++){
				int x=ManagerScript.getFieldRankID(0,i,player);
				if(x>0 && ManagerScript.getCardColor(x,player)==1 && ManagerScript.getCardType(x,player)==2){
					BodyScript.Targetable.Add(x+50*player);
				}
			}
			for(int i=0;i<2 && i<BodyScript.Targetable.Count;i++){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

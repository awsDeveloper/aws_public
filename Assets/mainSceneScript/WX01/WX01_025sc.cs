using UnityEngine;
using System.Collections;

public class WX01_025sc : MonoBehaviour {
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
			int lrigzoneNum=ManagerScript.getFieldAllNum(4,player);
			int Lcolor=ManagerScript.getCardColor(ManagerScript.getFieldRankID(4,lrigzoneNum-1,player),player);
			int trashNum=ManagerScript.getFieldAllNum(7,player);	
			for(int i=0;i<trashNum;i++){
				int x=ManagerScript.getFieldRankID(7,i,player);
				if(x>=0 && ManagerScript.getCardType(x,player)==2 && ManagerScript.getCardColor(x,player)==Lcolor){
					BodyScript.Targetable.Add(x+50*player);
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

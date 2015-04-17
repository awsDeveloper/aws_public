using UnityEngine;
using System.Collections;

public class WX01_043sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool costFlag=false;

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
		if(BodyScript.Ignition){
			BodyScript.Ignition=false;
			int costNum=ManagerScript.getEnaColorNum(3,player);
			costNum+=ManagerScript.MultiEnaNum(player);
			if(costNum>=1 && ManagerScript.getIDConditionInt(ID,player)==1){
				costFlag=true;
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				BodyScript.Cost[0]=0;
				BodyScript.Cost[1]=0;
				BodyScript.Cost[2]=0;
				BodyScript.Cost[3]=1;
				BodyScript.Cost[4]=0;
				BodyScript.Cost[5]=0;
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(8);
			}
		}
		
		if(BodyScript.effectTargetID.Count==0 && costFlag){
			costFlag=false;
			int deckNum=ManagerScript.getFieldAllNum(0,player);
			if(deckNum>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(22);
				for(int i=0; i<deckNum && i<2 ;i++){
					BodyScript.effectTargetID.Add(50*player);
					BodyScript.effectMotion.Add(2);
				}
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class WX02_006sc : MonoBehaviour {
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

		BodyScript.attackArts=true;
	}
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag && ManagerScript.getFieldAllNum(7,player)>=25){
			int target=1-player;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
				if(x>=0){
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(5);
				}
			}
			target=player;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
				if(x>=0){
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(5);
				}
			}
			if(BodyScript.effectTargetID.Count>0){
				BodyScript.effectFlag=true;
			}
		}
		field=ManagerScript.getFieldInt(ID,player);
	}
}

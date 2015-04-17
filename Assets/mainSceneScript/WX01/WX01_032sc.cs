using UnityEngine;
using System.Collections;

public class WX01_032sc : MonoBehaviour {
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
			for(int i=0;i<handNum;i++){
				int x=ManagerScript.getFieldRankID(2,i,1-player);
				if(x>=0)BodyScript.Targetable.Add(x+50*(1-player));
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				for(int i=0;i<2 &&i < BodyScript.Targetable.Count;i++){
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(19);
				}
			}
			if(handNum<=2){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(2);
			}
		}
		
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			int handNum=ManagerScript.getFieldAllNum(2,1-player);
			if(handNum>0){
				BodyScript.effectFlag=true;
				int rand=Random.Range(0,handNum);
				BodyScript.effectTargetID.Add(ManagerScript.getFieldRankID(2,rand,1-player)+50*(1-player));
				BodyScript.effectMotion.Add(19);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

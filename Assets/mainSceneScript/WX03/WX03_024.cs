using UnityEngine;
using System.Collections;

public class WX03_024 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	int effectTurn=-1;
	
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
			int target=player;
			int f=1;
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardLevel(x,target)==ManagerScript.getLrigLevel(target)){
					BodyScript.Targetable.Add(x+50*(target));
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(37);
			}
		}
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			effectTurn=ManagerScript.getTurn();
			
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(22);
		}
		
		if(ManagerScript.getTurn()>effectTurn && effectTurn!=-1 && ManagerScript.getTurnPlayer()==player){
			effectTurn=-1;
			ManagerScript.NoCostGrow=true;
		}
		
		//update
		field=ManagerScript.getFieldInt(ID,player);
	}
}

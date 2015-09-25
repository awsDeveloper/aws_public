using UnityEngine;
using System.Collections;

public class PR_030 : MonoBehaviour {
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
		//chant
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			if(ManagerScript.getFieldAllNum(3,player)>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(33);
			}
		}
		
		//after cost
		if(BodyScript.costBanish){
			BodyScript.costBanish=false;
			BodyScript.Targetable.Clear();
			
			int target=player;
			int f=0;
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(ManagerScript.getCardColor(x,target)==1 && ManagerScript.getCardType(x,target)==2)BodyScript.Targetable.Add(x+50*target);
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
		}
		
		//Update
		field=ManagerScript.getFieldInt(ID,player);
	}
}

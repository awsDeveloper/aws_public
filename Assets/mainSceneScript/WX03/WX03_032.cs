using UnityEngine;
using System.Collections;

public class WX03_032 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	int enaNum=0;
	
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.powerUpValue=3000;
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==3 && ManagerScript.getFieldAllNum(6,player)>enaNum && ManagerScript.getTurnPlayer()==player){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(ID+50*player);
			BodyScript.effectMotion.Add(34);
		}
		
		if(ManagerScript.getFieldInt(ID,player)==3 && BodyScript.Power>=15000 && !BodyScript.effectFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(ID+50*player);
			BodyScript.effectMotion.Add(5);
			
			int target=1-player;
			int f=3;
			int num=3;
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0)BodyScript.Targetable.Add(x+50*target);
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}
		}
		
		//update
		enaNum=ManagerScript.getFieldAllNum(6,player);
	
	}
}

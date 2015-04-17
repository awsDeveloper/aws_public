using UnityEngine;
using System.Collections;

public class WX03_046 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool chantFlag=false;
	
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.powerUpValue=5000;
	}
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int target=player;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
				if(x>=0){
					BodyScript.Targetable.Add(x+50*(target));
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(34);
				
				chantFlag=true;
				BodyScript.effectFlag=true;
				BodyScript.TargetIDEnable=true;
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(22);
				BodyScript.AntiCheck=true;
			}
		}		
		//after chant
		if(BodyScript.effectTargetID.Count==0 && chantFlag && BodyScript.TargetID.Count==1){
			chantFlag=false;
			if(!BodyScript.AntiCheck){
				int tID=BodyScript.TargetID[0];
				CardScript sc=ManagerScript.getCardScr(tID%50,tID/50);
				
				if(sc.Power>=15000 && !sc.lancer)
                {
                    BodyScript.effectMotion.Add((int)Motions.EnLancerEnd);
                    BodyScript.effectTargetID.Add(tID);
				}
			}
			else{
				BodyScript.AntiCheck=false;
			}

			BodyScript.TargetID.Clear();
			BodyScript.TargetIDEnable=false;
		}
		
		field=ManagerScript.getFieldInt(ID,player);
	}
}

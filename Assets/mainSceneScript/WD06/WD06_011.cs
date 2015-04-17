using UnityEngine;
using System.Collections;

public class WD06_011 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool effectFlag=false;
	
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
		//ignition
		if(BodyScript.Ignition){	
			BodyScript.Ignition=false;
			
			if(ManagerScript.getIDConditionInt(ID,player)==1){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(8);
				
				int f=5;
				int target=player;
				int num=ManagerScript.getNumForCard(f,target);
				
				int x=ManagerScript.getFieldRankID(f,num-1,target);
				if(x>=0){
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(16);
					BodyScript.effectTargetID.Add(50*target);
					BodyScript.effectMotion.Add(41);
				}
				
			}
		}
		
		//burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            int target = player;

            effectFlag = true;
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50 * target);
            BodyScript.effectMotion.Add((int)Motions.TopLifeGohand);
        }
		
		if(effectFlag && BodyScript.effectTargetID.Count==0){
			effectFlag=false;
			BodyScript.effectTargetID.Add(-1);
			BodyScript.effectMotion.Add(42);	
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(43);
		}
		
		//update
		field=ManagerScript.getFieldInt(ID,player);
	}
}

using UnityEngine;
using System.Collections;

public class PR_079 : MonoBehaviour {
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

		BodyScript.powerUpValue=-8000;
	}
	
	// Update is called once per frame
	void Update () {
		//chant
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			if(ManagerScript.getFieldAllNum(3,player)>0){
				BodyScript.TargetIDEnable=false;
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(33);
			}
		}
		
		//after cost
		if(BodyScript.costBanish){
			BodyScript.costBanish=false;
			BodyScript.Targetable.Clear();
			
			int target=1-player;
			int f=3;
			int num=ManagerScript.getNumForCard(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0)
					BodyScript.Targetable.Add(x+50*target);
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.TargetIDEnable=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(21);
			}
		}

		//check
		for(int i=0;i<BodyScript.TargetID.Count;i++){

			int tID=BodyScript.TargetID[i];

			if(ManagerScript.getFieldInt(tID%50,tID/50)!=3)
			{
				ManagerScript.upCardPower(tID%50,tID/50,-BodyScript.powerUpValue);
				BodyScript.TargetID.RemoveAt(i);
				i--;
			}
		}
		
		//Update
		field=ManagerScript.getFieldInt(ID,player);
	}
}

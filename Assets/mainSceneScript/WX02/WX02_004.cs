using UnityEngine;
using System.Collections;

public class WX02_004 : MonoBehaviour {
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
		BodyScript.powerUpValue=-10000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ID==ManagerScript.getLrigID(player)){
			if(BodyScript.Ignition){
				BodyScript.Ignition=false;
				int handNum=ManagerScript.getFieldAllNum(2,player);
				for(int i=0;i<handNum;i++){
					int x=ManagerScript.getFieldRankID(2,i,player);
					if(x>0 && ManagerScript.getCardColor(x,player)==BodyScript.CardColor && ManagerScript.getCardType(x,player)==2){
						BodyScript.Targetable.Add(x+50*player);
					}
				}
				if(BodyScript.Targetable.Count>=1){
					costFlag=true;
					BodyScript.effectFlag=true;
					BodyScript.TargetIDEnable=false;
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(19);
					BodyScript.effectTargetID.Add(ID+player*50);
					BodyScript.effectMotion.Add(17);
					BodyScript.Cost[0]=0;
					BodyScript.Cost[1]=0;
					BodyScript.Cost[2]=0;
					BodyScript.Cost[3]=0;
					BodyScript.Cost[4]=0;
					BodyScript.Cost[5]=1;
				}
			}
		}
		if(BodyScript.effectTargetID.Count==0 && costFlag){
			costFlag=false;

			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,1-player);
				if(x>0){
					BodyScript.Targetable.Add(x+50*(1-player));
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.TargetIDEnable=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(21);
			}
		}
		if(BodyScript.TargetID.Count>0){
			for(int i=0;i<BodyScript.TargetID.Count;i++){
				int x=BodyScript.TargetID[i];
				if(ManagerScript.getFieldInt(x%50,x/50)!=3 || ManagerScript.getPhaseInt()==7){
					ManagerScript.upCardPower(x%50,x/50,-BodyScript.powerUpValue);
					BodyScript.TargetID.RemoveAt(i);
					i--;
				}
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class WX02_017sc : MonoBehaviour {
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
			int target=player;
			int Num=ManagerScript.getFieldAllNum(5,target);
			int x=ManagerScript.getFieldRankID(5,Num-1,target);
			if(x>=0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(x+50*target);
				BodyScript.effectMotion.Add(28);
				
				CardScript sc=ManagerScript.getCardScr(x,target);
				if(sc.BurstIcon==1){
					target=1-player;
					for(int i=0;i<3;i++){
						int y=ManagerScript.getFieldRankID(3,i,target);
						if(y>=0 && ManagerScript.getCardPower(y,target)<=10000){
							BodyScript.effectTargetID.Add(y+50*target);
							BodyScript.effectMotion.Add(5);
						}
					}
				}
			}
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

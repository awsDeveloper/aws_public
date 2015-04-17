using UnityEngine;
using System.Collections;

public class WX01_035sc : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool costFlag=false;
	bool IgnitionFlag=false;

	// Use this for initialization
	void Start () {
		GameObject Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;

		GameObject Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(BodyScript.Ignition && !IgnitionFlag){	
			int costNum=ManagerScript.getEnaColorNum(1,player);
			costNum+=ManagerScript.MultiEnaNum(player);
			if(costNum>=1 && ManagerScript.getIDConditionInt(ID,player)==1){
				costFlag=true;
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				BodyScript.Cost[0]=0;
				BodyScript.Cost[1]=1;
				BodyScript.Cost[2]=0;
				BodyScript.Cost[3]=0;
				BodyScript.Cost[4]=0;
				BodyScript.Cost[5]=0;
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(8);
			}
			else BodyScript.Ignition=false;
		}
		if(BodyScript.effectTargetID.Count==0 && costFlag){
			BodyScript.effectFlag=true;
			costFlag=false;
			BodyScript.Ignition=false;
			BodyScript.Targetable.Clear();
			
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,1-player);
				if(x>0){
					BodyScript.Targetable.Add(x+(1-player)*50);
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(16);
			}
		}
		IgnitionFlag=BodyScript.Ignition;
	}
}

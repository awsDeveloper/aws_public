using UnityEngine;
using System.Collections;

public class PR_038 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool costFlag=false;
	bool IgnitionFlag=false;

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
		if(BodyScript.Ignition && !IgnitionFlag){	
			if(ManagerScript.getIDConditionInt(ID,player)==1){
				costFlag=true;
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(8);
			}
			else BodyScript.Ignition=false;
		}
		
		if(BodyScript.effectTargetID.Count==0 && costFlag){
			costFlag=false;
			BodyScript.Ignition=false;
			
			int target=player;
			int f=0;
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(checkClass(x,target) && ManagerScript.getCardLevel(x,target)==2)BodyScript.Targetable.Add(x+50*target);
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
		}
		IgnitionFlag=BodyScript.Ignition;
	}
	
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return (c[0]==2 && c[1]==2 );
	}
}

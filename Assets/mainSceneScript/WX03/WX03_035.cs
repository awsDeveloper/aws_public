using UnityEngine;
using System.Collections;

public class WX03_035 : MonoBehaviour {
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
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(BodyScript.Ignition){	
			BodyScript.Ignition=false;
			
			int target=player;
			int f=3;
			
			int num=ManagerScript.getFieldAllNum(f,target);
			if(f==3)num=3;
			
			if(ManagerScript.getIDConditionInt(ID,player)==1){
				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(f,i,target);
					if(x!=ID && x>=0 && checkClass(x,target))BodyScript.Targetable.Add(x+50*target);
				}
				if(BodyScript.Targetable.Count>0){
					costFlag=true;
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(7);
					BodyScript.effectTargetID.Add(ID+50*player);
					BodyScript.effectMotion.Add(8);
				}
			}
		}
		
		if(BodyScript.effectTargetID.Count==0 && costFlag){
			costFlag=false;
			BodyScript.Targetable.Clear();
			
			int target=1-player;
			int f=3;
			
			int num=ManagerScript.getFieldAllNum(f,target);
			if(f==3)num=3;
			
			
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardLevel(x,target)<=3){
					BodyScript.Targetable.Add(x+(target)*50);
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}
		}
	}
	
	bool checkClass(int x,int target){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,target);
		return (c[0]==3 && c[1]==1 );
	}
}

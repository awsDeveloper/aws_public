using UnityEngine;
using System.Collections;

public class WD07_007 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool flag_b=false;
	bool flag_w=false;

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
			int f=0;
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<4;i++){
				int x=ManagerScript.getFieldRankID(f,num-1-i,target);
				if(x>=0){
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(7);
				}
			}
			
			if(BodyScript.effectTargetID.Count>0){
				BodyScript.effectFlag=true;
				flag_b=checkColor(5);
				flag_w=checkColor(1);
			}
		}
		
		if(BodyScript.effectTargetID.Count==0){			
			if(flag_b){
				flag_b=false;
				
				int target=player;
				int f=7;
				int num=ManagerScript.getFieldAllNum(f,target);

				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(f,i,target);
					if(x>=0 && ManagerScript.getSigniColor(x,target)==5){
						BodyScript.Targetable.Add(x+50*target);
					}
				}
				
				if(BodyScript.Targetable.Count>0){
					BodyScript.effectTargetID.Add(-2);
					BodyScript.effectMotion.Add(16);
				}
			}
			else if(flag_w){
				flag_w=false;
				BodyScript.Targetable.Clear();
				
				int target=player;
				int f=0;
				int num=ManagerScript.getFieldAllNum(f,target);

				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(f,i,target);
					if(x>=0 && ManagerScript.getSigniColor(x,target)==1){
						BodyScript.Targetable.Add(x+50*target);
					}
				}
				
				if(BodyScript.Targetable.Count>0){
					BodyScript.effectTargetID.Add(-2);
					BodyScript.effectMotion.Add(16);
				}
			}
		}
		
		field=ManagerScript.getFieldInt(ID,player);	
	}
	
	bool checkColor(int color){
		
		for(int i=0;i<BodyScript.effectTargetID.Count;i++){
			int id=BodyScript.effectTargetID[i];
			int c=ManagerScript.getCardColor(id%50,id/50);
			
			if(color==c)return true;
		}
		
		return false;
	}
}

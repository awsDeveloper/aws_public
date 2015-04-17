using UnityEngine;
using System.Collections;

public class WD07_009 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool costFlag=false;
	
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.targetableDontRemove=true;
	}
	
	// Update is called once per frame
	void Update () {
		//ignition
		if(BodyScript.Ignition){	
			BodyScript.Ignition=false;
			
			if(ManagerScript.getIDConditionInt(ID,player)==1){
				costFlag=true;
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(8);
				
			}
		}
		
		if(costFlag && BodyScript.effectTargetID.Count==0){
			costFlag=false;
			
			int target=player;
			int f=0;
			
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(f,num-i-1,target);
				if(x>=0){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0){
				if(checkTargetable()){
					BodyScript.effectTargetID.Add(-2);
					BodyScript.effectMotion.Add(47);
					BodyScript.TargetIDEnable=true;
				}
				
				for(int i=0;i<BodyScript.Targetable.Count;i++){
					BodyScript.effectTargetID.Add(BodyScript.Targetable[i]);
					BodyScript.effectMotion.Add(14);
				}
			}
		}
		
		if(BodyScript.TargetID.Count>0){
			
			int x=BodyScript.TargetID[0];
			
			if(isSearchable(x%50,x/50,1) ||isSearchable(x%50,x/50,5)){
				
				for(int i=1;i<BodyScript.effectTargetID.Count;i++){
					if(BodyScript.effectTargetID[i]==BodyScript.TargetID[0]){
						BodyScript.effectTargetID.RemoveAt(i);
						BodyScript.TargetID.Clear();
						BodyScript.TargetIDEnable=false;
						break;
					}
				}
			}
			else {
				BodyScript.TargetID.Clear();
				BodyScript.effectTargetID[0]=-2;			
			}
		}
		
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			int target=player;
			int f=0;
			
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getSigniColor(x,target)==5){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
		}
		
		//update
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	bool checkTargetable(){
		for(int i=0;i<BodyScript.Targetable.Count;i++){
			int x=BodyScript.Targetable[i];
			if(isSearchable(x%50,x/50,1) || isSearchable(x%50,x/50,5))return true;
		}
		return false;
	}
	
	bool isSearchable(int x,int target,int color){
		int c=ManagerScript.getSigniColor(x,target);
		return color==c;
	}
}

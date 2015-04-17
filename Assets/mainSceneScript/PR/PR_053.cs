using UnityEngine;
using System.Collections;

public class PR_053 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool chantFlag=false;
	bool flag=false;
	
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
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(22);
			BodyScript.AntiCheck=true;
			chantFlag=true;
			
		}
		
		if(chantFlag && BodyScript.effectTargetID.Count==0){
			chantFlag=false;
			if(BodyScript.AntiCheck){
				BodyScript.AntiCheck=false;
			}
			else{
				flag=true;
				
				int target=1-player;
				BodyScript.effectSelecter=target;
				int f=6;
				int num=ManagerScript.getFieldAllNum(f,target);
				
				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(f,i,target);
					if(x>=0){
						BodyScript.Targetable.Add(x+50*(target));
					}
				}
				
				if(BodyScript.Targetable.Count>0){
					while(num-BodyScript.effectTargetID.Count>4){
						BodyScript.effectTargetID.Add(-1);
						BodyScript.effectMotion.Add(7);
					}
					
					BodyScript.effectTargetID.Add(50*target);
					BodyScript.effectMotion.Add(20);					
				}			
			}
		}
		
		if(flag && BodyScript.effectTargetID.Count==0){
			flag=false;
			
			int target=player;
			BodyScript.effectSelecter=target;
			int f=6;
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0){
					BodyScript.Targetable.Add(x+50*(target));
				}
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(22);
				
				while(num-(BodyScript.effectTargetID.Count-1) > 4 ){
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(7);
				}
				
				BodyScript.effectTargetID.Add(50*target);
				BodyScript.effectMotion.Add(20);					
			}						
		}
		
		field=ManagerScript.getFieldInt(ID,player);
	}
}

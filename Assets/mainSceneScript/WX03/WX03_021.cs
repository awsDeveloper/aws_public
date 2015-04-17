using UnityEngine;
using System.Collections;

public class WX03_021 : MonoBehaviour {
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
		//effect
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int target=player;
			int f=3;
			int num=ManagerScript.getFieldAllNum(f,target);
			if(f==3)num=3;
			
			int count=0;
						
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(7);
					
					if(checkClass(x,target))count++;
				}
			}
			
			if(count==3){
				target=1-player;
				f=5;
				
				for(int i=0;i<2;i++){
					int x=ManagerScript.getFieldRankID(f,ManagerScript.getFieldAllNum(f,target)-i-1,target);
					if(x>=0){
						BodyScript.effectTargetID.Add(x+50*target);
						BodyScript.effectMotion.Add(28);
					}
				}
			}
			else {
				BodyScript.effectFlag=false;
				BodyScript.effectTargetID.Clear();
				BodyScript.effectMotion.Clear();
			}
		}
		
		//update
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	bool checkClass(int x,int target){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,target);
		return (c[0]==3 && c[1]==1 );
	}
}

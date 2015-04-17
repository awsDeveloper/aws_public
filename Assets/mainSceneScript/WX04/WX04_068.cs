using UnityEngine;
using System.Collections;

public class WX04_068 : MonoBehaviour {
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
	}
	
	// Update is called once per frame
	void Update () {
		//cip
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag)
		{
			inTarget();

			if(BodyScript.Targetable.Count>0)
			{
				BodyScript.Targetable.Clear();

				int target=player;
				int f=2;
				int num=ManagerScript.getFieldAllNum(f,target);

				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(f,i,target);
					if(x>=0){
						BodyScript.Targetable.Add(x+50*target);
					}		
				}
				
				if(BodyScript.Targetable.Count>0){
					BodyScript.DialogFlag=true;
				}
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
				costFlag=true;
			}
			else BodyScript.Targetable.Clear();
			
			BodyScript.messages.Clear();
		}		
		
		//after cost
		if(costFlag && BodyScript.effectTargetID.Count==0){
			costFlag=false;

			effect();
		}

		//update
		field=ManagerScript.getFieldInt(ID,player);
	}

	void effect(){
		inTarget();

		if(BodyScript.Targetable.Count>0)
		{
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(-1);
			BodyScript.effectMotion.Add(7);
			BodyScript.effectTargetID.Add(50*(1-player));
			BodyScript.effectMotion.Add(20);
		}
	}

	void inTarget(){
		int target=1-player;
		int f=6;
		int num=ManagerScript.getNumForCard(f,target);
		
		for (int i = 0; i < num; i++) {
			int x=ManagerScript.getFieldRankID(f,i,target);
			
			if(x>=0 && ManagerScript.getCardScr(x,target).MultiEnaFlag){
				BodyScript.Targetable.Add(x+50*target);
			}
		}
	}
}

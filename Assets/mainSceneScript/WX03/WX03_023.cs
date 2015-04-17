using UnityEngine;
using System.Collections;

public class WX03_023 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool costFlag=false;
	bool upFlag=false;
		
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.powerUpValue=3000;
	}
	
	// Update is called once per frame
	void Update () {
		//always
		if(field==3 && upCondition()){
            if (!upFlag)
            {
                ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower + BodyScript.powerUpValue);
                upFlag = true;
            }
        }
        else if (upFlag)
        {
            ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower);
            upFlag = false;
        }		
		
		//cip
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			BodyScript.Cost[0]=1;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=1;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=0;
			
			if(ManagerScript.checkCost(ID,player)){
				 BodyScript.DialogFlag=true;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				costFlag=true;
			}
			else BodyScript.Targetable.Clear();
			
			BodyScript.messages.Clear();
		}
		
		if(costFlag && BodyScript.effectTargetID.Count==0){
			costFlag=false;
			
			int target=player;
			int f=0;
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(ManagerScript.getCardScr(x,target).Name.Contains("手弾　アヤボン")){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(6);
				BodyScript.effectTargetID.Add(player*50);
				BodyScript.effectMotion.Add(24);
			}
		}
		
		field=ManagerScript.getFieldInt(ID,player);	
	}
	
	bool upCondition(){
		int target=player;
		for(int i=0;i<3;i++){
			int x=ManagerScript.getFieldRankID(3,i,target);
			if(x>=0 && ManagerScript.getCardScr(x,target).Name.Contains("手弾　アヤボン"))return true;
		}
		
		return false;
	}
}

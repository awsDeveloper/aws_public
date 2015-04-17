using UnityEngine;
using System.Collections;

public class WX03_050 : MonoBehaviour {
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
			int trashNum=ManagerScript.getFieldAllNum(7,player);
			for(int i=0;i<trashNum;i++){
				int x=ManagerScript.getFieldRankID(7,i,player);
				if(checkClass(x,player)){
					BodyScript.Targetable.Add(x+50*player);
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
		}
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			BodyScript.DialogCountMax=getClassNum(7,player);
			if(BodyScript.DialogCountMax>2)BodyScript.DialogCountMax=2;
			
			if(BodyScript.DialogCountMax>0){
				BodyScript.DialogFlag=true;
				BodyScript.DialogNum=1;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			int count=int.Parse(BodyScript.messages[0]);
			BodyScript.messages.Clear();
			
			if(count>0){
				int trashNum=ManagerScript.getFieldAllNum(7,player);
				for(int i=0;i<trashNum;i++){
					int x=ManagerScript.getFieldRankID(7,i,player);
					if(checkClass(x,player)){
						BodyScript.Targetable.Add(x+50*player);
					}
				}
				
				if(BodyScript.Targetable.Count>0){
					for(int i=0;i<count && i<BodyScript.Targetable.Count;i++){
						BodyScript.effectTargetID.Add(-2);
						BodyScript.effectMotion.Add(16);
						BodyScript.effectFlag=true;
					}
				}
			}
		}		
		
		
		field=ManagerScript.getFieldInt(ID,player);	
	}
	
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return c[0]==4 && c[1]==1;
	}
	
	int getClassNum(int f,int target){
		int count=0;
		
		int num=ManagerScript.getFieldAllNum(f,target);
		if(f==3)num=3;
		
		for(int i=0;i<num;i++){
			int x=ManagerScript.getFieldRankID(f,i,target);
			if(checkClass(x,target))count++;
		}
		
		return count;
	}
}

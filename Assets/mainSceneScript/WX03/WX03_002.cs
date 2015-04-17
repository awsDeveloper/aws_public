using UnityEngine;
using System.Collections;

public class WX03_002 : MonoBehaviour {
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
		//change cost
		if(ManagerScript.getLrigColor(1-player)==5){
			BodyScript.Cost[0]=0;
			BodyScript.Cost[1]=2;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=0;
		}
		else{
			BodyScript.Cost[0]=3;
			BodyScript.Cost[1]=2;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=0;
		}
		
		//effect
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int target=1-player;
			int f=3;
			int num=ManagerScript.getFieldAllNum(f,target);
			if(f==3)num=3;
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && !checkClass(x,target) && ManagerScript.getCardType(x,target)==2){
					BodyScript.Targetable.Add(x+50*(target));
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(7);
			}
		}
		
		//update
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return (c[0]==4 && c[1]==0);
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX02_040sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	List<int> equipList=new List<int>();
	int burstCount=-1;
	bool chantFlag=false;
	int phase=-1;
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=2000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		//check equip
		checkEquip();
		//cip
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int target=player;
			bool flag=false;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
				if(checkClass(x,target))flag=true;
			}
			if(flag){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(0);
				BodyScript.effectMotion.Add(22);
				BodyScript.AntiCheck=true;
				chantFlag=true;
			}
		}
		if(chantFlag && BodyScript.effectTargetID.Count==0){
			chantFlag=false;
			if(!BodyScript.AntiCheck){
				int target=player;
//				bool flag=false;
				for(int i=0;i<3;i++){
					int x=ManagerScript.getFieldRankID(3,i,target);
					if(checkClass(x,target)){
						equip(x,target);
						burstCount=-1;
					}
				}
			}
			else BodyScript.AntiCheck=false;
		}
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			burstCount=2;
		}
		if(burstCount>0 && ManagerScript.getPhaseInt()==7 && phase!=7){
			burstCount--;
		}
		field=ManagerScript.getFieldInt(ID,player);
		phase= ManagerScript.getPhaseInt();
	}
	
	void equip(int x,int eplayer){
		if(x<0)return;
		CardScript sc=ManagerScript.getCardScr(x,eplayer);
		if(!sc.lancer){
			sc.lancer=true;
			equipList.Add(x+50*eplayer);
		}
	}
	void dequip(int index){
		if(index>=equipList.Count)return;
		int px=equipList[index];
		CardScript sc=ManagerScript.getCardScr(px%50,px/50);
		sc.lancer=false;
		equipList.RemoveAt(index);
	}
	void checkEquip(){
		int target=player;
		int equipField=3;
//		int fieldAll=ManagerScript.getFieldAllNum(equipField,player);
		//check situation
		if(burstCount>0){
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(equipField,i,target);
				if(!checkExist(x,target))equip(x,target);
			}			
		}
		else if(burstCount==0){
			while(equipList.Count>0){
				dequip(0);
			}
		}
		//equip target check
		if(equipList.Count>0){
			for(int i=0;i<equipList.Count;i++){
				int x=equipList[i];
				if(ManagerScript.getFieldInt(x%50,x/50)!=equipField){
					dequip(i);
					i--;
				}				
			}
		}
	}
	bool checkExist(int x,int player){
		for(int i=0;i<equipList.Count;i++){
			if(x+50*player==equipList[i])return true;
		}
		return false;
	}
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return (c[0]==2 && c[1]==2)||(c[0]==5 && (c[1]==1 || c[1]==2));
	}
}

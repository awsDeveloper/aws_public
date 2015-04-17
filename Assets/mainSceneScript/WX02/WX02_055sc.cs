using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX02_055sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	List<int> doubleCrashID=new List<int>();
	bool chantFlag=false;
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
		 //cip
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int target=player;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
				if(checkClass(x,target)){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			if(BodyScript.Targetable.Count>0){
				chantFlag=true;
				BodyScript.effectFlag=true;
				BodyScript.TargetIDEnable=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(22);
				BodyScript.AntiCheck=true;
			}
		}
		//cip after cost
		if(BodyScript.effectTargetID.Count==0 && chantFlag && BodyScript.TargetID.Count==1){
			chantFlag=false;
			if(!BodyScript.AntiCheck){
				doubleCrashID.Add(BodyScript.TargetID[0]);
	
				int end=doubleCrashID.Count-1;
				CardScript sc=ManagerScript.getCardScr(doubleCrashID[end]%50,doubleCrashID[end]/50);
				if(!sc.DoubleCrash)sc.DoubleCrash=true;
				else doubleCrashID.RemoveAt(end);			
			}
			else{
				BodyScript.AntiCheck=false;
			}
			BodyScript.TargetID.Clear();
			BodyScript.TargetIDEnable=false;
		}
		if(doubleCrashID.Count>0){
			for(int i=0;i<doubleCrashID.Count;i++){
				if(ManagerScript.getFieldInt(doubleCrashID[i]%50,doubleCrashID[i]/50)!=3 || ManagerScript.getPhaseInt()==7){
					CardScript sc=ManagerScript.getCardScr(doubleCrashID[i]%50,doubleCrashID[i]/50);
					sc.DoubleCrash=false;
					doubleCrashID.RemoveAt(i);
					i--;
				}
			}
		}
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	bool checkClass(int x,int cplayer){
/*		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return (c[0]==1 && c[1]==1)||(c[0]==2 && (c[1]==0 || c[1]==1));*/
        return ManagerScript.checkClass(x, cplayer, cardClassInfo.精羅_鉱石)
            || ManagerScript.checkClass(x, cplayer, cardClassInfo.精羅_宝石)
            || ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_ウェポン); 
	}
}

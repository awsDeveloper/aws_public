using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PR_020sc : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool DialogFlag=false;
	const int selectMax=1;
	List<int> checkID=new List<int>();
	bool chantFlag=false;
	bool effectFlag=false;
	
	// Use this for initialization
	void Start () {
		GameObject Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		GameObject Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.checkStr.Add("サーチ");
		BodyScript.checkStr.Add("エナチャージ");
		
		for(int i=0;i<BodyScript.checkStr.Count;i++){
			BodyScript.checkBox.Add(false);

		}
  
     }
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			if(checkID.Count==0){
				BodyScript.DialogFlag=true;
				BodyScript.DialogCountMax=1;
				BodyScript.DialogNum=2;
			}
			else{
				effect_1();
				effect_2();
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				for(int i=0;i<BodyScript.checkBox.Count;i++){
					if(BodyScript.checkBox[i])checkID.Add(i);
				}
				chantFlag=true;
				effect_1();
				effect_2();
			}
			else destory();
			
			BodyScript.messages.Clear();
		}
		
		
		if(BodyScript.effectTargetID.Count==0 && effectFlag){
			effectFlag=false;
			if(!BodyScript.AntiCheck){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
			else BodyScript.AntiCheck=false;
		}
		
		if(BodyScript.effectTargetID.Count==0 && chantFlag){
			chantFlag=false;
			destory();
		}
		
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	void effect_1(){
		if(BodyScript.checkBox[0]){
			int num=ManagerScript.getFieldAllNum(0,player);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(0,i,player);
				if(x>=0 && ManagerScript.getCardPower(x,player)>=10000)
					BodyScript.Targetable.Add(x+50*player);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(22);
				BodyScript.AntiCheck=true;
				effectFlag=true;
			}
		}
	}
	void effect_2(){
		if(BodyScript.checkBox[1]){
			for(int i=0;i<2 && i<ManagerScript.getFieldAllNum(0,player);i++){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(26);
			}
		}
	}
	
	void destory(){
		while(checkID.Count>0){
			BodyScript.checkBox[checkID[0]]=false;
			checkID.RemoveAt(0);
		}
	}
}

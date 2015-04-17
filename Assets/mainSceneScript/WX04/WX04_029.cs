using UnityEngine;
using System.Collections;

public class WX04_029 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool effectFlag=false;
	int attackerID=-1;
	
	// Use this for initialization
	void Start () {
		GameObject Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=1000;
		
		GameObject Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		//dialog 2
		BodyScript.checkStr.Add("サーチ");
		BodyScript.checkStr.Add("リクルート");
		
		for(int i=0;i<BodyScript.checkStr.Count;i++){
			BodyScript.checkBox.Add(false);		
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			effectFlag=true;
			BodyScript.DialogFlag=true;
			BodyScript.DialogNum=2;
			BodyScript.DialogCountMax=1;
		}

		//誘発のチェック
		if(attackerID!=-1 && !ManagerScript.isEffectFlagUp())
		{
			if(ManagerScript.getFieldInt(ID,player) ==3)
			{
				int target=player;
				int f=3;
				int num=ManagerScript.getNumForCard(f,target);
				
				for (int i = 0; i < num; i++) {
					int x=ManagerScript.getFieldRankID(f,i,target);
					
					if(x>=0 && checkClass(x,target)){
						BodyScript.effectFlag=true;
						BodyScript.effectTargetID.Add(x+target*50);
						BodyScript.effectMotion.Add(34);
					}
				}
				
				if(ManagerScript.getFieldRankID(f, ManagerScript.getAttackFrontRank(), player)==-1)
				{
					BodyScript.DialogFlag=true;
					BodyScript.DialogNum=0;
				}
			}

			attackerID=-1;
		}

		//attackerID のセット
		if(ManagerScript.getFieldInt(ID,player) ==3 && ManagerScript.getAttackerID()!=-1 && ManagerScript.getAttackerID()/50==1-player)
		{
			attackerID=ManagerScript.getAttackerID();
			ManagerScript.stopFlag=true;
		}
		

		//receive
		if(BodyScript.messages.Count>0){
			if(effectFlag){
				effectFlag=false;
				
				if(BodyScript.messages[0].Contains("Yes")){
					effect_1();
					effect_2();
				}
			}
			else{
				if(BodyScript.messages[0].Contains("Yes")){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(ID + 50*player);
					BodyScript.effectMotion.Add(55);					
				}
			}
			
			BodyScript.messages.Clear();
		}
		
		//UpDate
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	void effect_1(){
		if(BodyScript.checkBox[0]){
			int target=player;
			int f=0;
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for (int i = 0; i < num; i++) {
				int x=ManagerScript.getFieldRankID(f,i,target);
				
				if(x>=0 && checkClass(x,target)){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			
			if(BodyScript.Targetable.Count>0)
			{
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
		}
	}

	void effect_2(){
		if(BodyScript.checkBox[1]){
			int target=player;
			int f=0;
			int num=ManagerScript.getFieldAllNum(f,target);

			for (int i = 0; i < num; i++) {
				int x=ManagerScript.getFieldRankID(f,i,target);

				if(x>=0 && checkClass(x,target)){
					BodyScript.Targetable.Add(x+50*target);
				}
			}

			if(BodyScript.Targetable.Count>0)
			{
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(6);
				BodyScript.effectTargetID.Add(target*50);
				BodyScript.effectMotion.Add(24);
			}
		}
	}

	bool checkClass (int x, int cplayer)
	{
		if (x < 0)
			return false;
		int[] c = ManagerScript.getCardClass (x, cplayer);
		return (c [0] == 3 && c [1] == 2);
	}
}

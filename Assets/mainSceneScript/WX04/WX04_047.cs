using UnityEngine;
using System.Collections;

public class WX04_047 : MonoBehaviour {
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
		BodyScript.powerUpValue=1000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		//dialog 2
		BodyScript.checkStr.Add("原子1枚");
		BodyScript.checkStr.Add("2枚");
		
		for(int i=0;i<BodyScript.checkStr.Count;i++){
			BodyScript.checkBox.Add(false);		
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		//ignition
		if(BodyScript.Ignition){
			BodyScript.Ignition=false;

			if(ManagerScript.getIDConditionInt(ID,player)==1){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(8);

				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(2);
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(2);

				costFlag=true;
			}
		}

		if(costFlag && BodyScript.effectTargetID.Count==0)
		{
			costFlag=false;

			targetIn_1();

			if(BodyScript.Targetable.Count>0){
				BodyScript.Targetable.Clear();

				BodyScript.DialogFlag=true;
				BodyScript.DialogNum=2;
				BodyScript.DialogCountMax=1;
			}
			else
				effect_2();
		}

		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				effect_1();
				effect_2();
			}
			else{
				BodyScript.DialogFlag=true;
				BodyScript.DialogNum=2;
				BodyScript.DialogCountMax=1;

			}
			BodyScript.messages.Clear();
		}

	}

	void effect_1(){
		if(BodyScript.checkBox[0]){

			targetIn_1();

			if(BodyScript.Targetable.Count>0)
			{
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
			}
		}
	}

	void effect_2(){
		if(BodyScript.checkBox[1]){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(45);
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(45);
		}
	}

	void targetIn_1()
	{
		int target=player;
		int f=2;
		int num=ManagerScript.getFieldAllNum(f,target);
		
		for (int i = 0; i < num; i++) {
			int x=ManagerScript.getFieldRankID(f,i,target);
			
			if(x>=0 && checkClass(x,target)){
				BodyScript.Targetable.Add(x+50*target);
			}
		}
	}
	
	bool checkClass (int x, int cplayer)
	{
		if (x < 0)
			return false;
		int[] c = ManagerScript.getCardClass (x, cplayer);
		return (c [0] == 2 && c [1] == 3);
	}
}

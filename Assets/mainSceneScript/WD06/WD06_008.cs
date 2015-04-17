using UnityEngine;
using System.Collections;

public class WD06_008 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool effectFlag=false;
	bool chantFlag=false;

	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.attackArts=true;
		BodyScript.notMainArts=true;
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			BodyScript.DialogFlag=true;
			BodyScript.DialogNum=1;			
			BodyScript.DialogCountMax=4;
			BodyScript.DialogStr="宣言するレベル";
			BodyScript.DialogStrEnable=true;
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			int count=int.Parse(BodyScript.messages[0]);
			BodyScript.messages.Clear();
			
			chantFlag=true;
			
			int f=5;
			int target=player;
			int num=ManagerScript.getNumForCard(f,target);
			int x=ManagerScript.getFieldRankID(f,num-1,target);
			
			if(x>=0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(x+50*target);
				BodyScript.effectMotion.Add(29);
				BodyScript.effectTargetID.Add(x+50*target);
				BodyScript.effectMotion.Add(30);
				
				if(ManagerScript.getCardLevel(x,target)==count){
					ManagerScript.notCrashFlag=true;
					effectFlag=true;
				}
			}
		}
		
		if(chantFlag && BodyScript.effectTargetID.Count==0){
			chantFlag=false;
			if(ManagerScript.notCrashFlag)ManagerScript.messageShow("Success");
			else ManagerScript.messageShow("Failure");
		}
		
		if(effectFlag && ManagerScript.getPhaseInt()==7){
			effectFlag=false;
			
			int f=5;
			int target=player;
			int num=ManagerScript.getNumForCard(f,target);
			int x=ManagerScript.getFieldRankID(f,num-1,target);
			
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(x+50*target);
			BodyScript.effectMotion.Add(7);
		}
		
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

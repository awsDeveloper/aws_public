using UnityEngine;
using System.Collections;

public class WD03_006sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
//	bool DialogFlag=false;
	int count=1;

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
			BodyScript.DialogFlag=true;
			BodyScript.DialogNum=1;
			BodyScript.DialogCountMax=4;
			BodyScript.DialogStr="宣言するレベル";
			BodyScript.DialogStrEnable=true;
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			count=int.Parse(BodyScript.messages[0]);
			BodyScript.messages.Clear();
			
			int handNum=ManagerScript.getFieldAllNum(2,1-player);

			//open
			BodyScript.effectFlag = true;
			BodyScript.effectTargetID.Add (50*(1-player));
			BodyScript.effectMotion.Add (66);

			for(int i=0;i<handNum;i++){

				int x=ManagerScript.getFieldRankID(2,i,1-player);

				if(x>0 && ManagerScript.getCardLevel(x,1-player)==count){
					BodyScript.effectTargetID.Add(x+50*(1-player));
					BodyScript.effectMotion.Add(19);
				}
			}

			//close
			BodyScript.effectTargetID.Add (50*(1-player));
			BodyScript.effectMotion.Add (67);
		}		

		field=ManagerScript.getFieldInt(ID,player);	
	}
	
}

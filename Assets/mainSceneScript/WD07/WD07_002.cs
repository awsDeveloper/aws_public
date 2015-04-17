using UnityEngine;
using System.Collections;

public class WD07_002 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int LrigID=-1;

	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.powerUpValue=-2000;
	}
	
	// Update is called once per frame
	void Update () {
		//cip
		if(LrigID!=ID && ManagerScript.getLrigID(player)==ID){
			BodyScript.changeColorCost(5,1);


			if(ManagerScript.checkCost(ID,player))
			{
				BodyScript.DialogFlag=true;
			}
		}

		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(17);
				
				int f=3;
				int target=1-player;
				int num=ManagerScript.getNumForCard(f,target);
				
				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(f,i,target);
					if(x>=0){
						BodyScript.effectTargetID.Add(x+50*target);
						BodyScript.effectMotion.Add(34);
					}
				}			
			}

			BodyScript.messages.Clear();
		}

		//update
		LrigID=ManagerScript.getLrigID(player);
	
	}
}

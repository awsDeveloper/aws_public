using UnityEngine;
using System.Collections;

public class WX02_066 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
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
				BodyScript.effectTargetID.Add(0);
				BodyScript.effectMotion.Add(22);
				int target=player;
				for(int i=0;i<3;i++){
					int x=ManagerScript.getFieldRankID(3,i,target);
					if(checkClass(x,target)){
						BodyScript.effectTargetID.Add(50*target);
						BodyScript.effectMotion.Add(26);
					}
				}
			}
			else BodyScript.AntiCheck=false;
		}
		field=ManagerScript.getFieldInt(ID,player);
	}

	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return (c[0]==2 && c[1]==2)||(c[0]==5 && (c[1]==1 || c[1]==2));
	}
}

using UnityEngine;
using System.Collections;

public class WX03_029 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool costFlag=false;
	bool upFlag=false;
	
	public int powerUpSize=2000;
	public int powerUpLevel=1;
		
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
		//always
		if(field==3 && upCondition()){
			if(!upFlag){
				BodyScript.Power+=powerUpSize;
				upFlag=true;
			}
		}
		else if(upFlag){
			BodyScript.Power-=powerUpSize;
			upFlag=false;
		}
				
		field=ManagerScript.getFieldInt(ID,player);	
	}
	
	bool upCondition(){
		int target=player;
		for(int i=0;i<3;i++){
			int x=ManagerScript.getFieldRankID(3,i,target);
			if(checkClass(x,target) && ManagerScript.getCardLevel(x,target)==powerUpLevel)return true;
		}
		
		return false;
	}
	
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return c[0]==3 && c[1]==0;
	}

}

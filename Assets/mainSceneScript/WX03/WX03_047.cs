using UnityEngine;
using System.Collections;

public class WX03_047 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;

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
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && ManagerScript.getFieldAllNum(3,player)<3){
			int target=player;
			int f=7;
			int num=ManagerScript.getFieldAllNum(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardLevel(x,target)==1 && checkClass(x,target)){
					BodyScript.Targetable.Add(x+50*(target));
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(35);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
	
	bool checkClass(int x,int target){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,target);
		return (c[0]==3 && c[1]==1 );
	}
}

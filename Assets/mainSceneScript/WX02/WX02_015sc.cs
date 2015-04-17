using UnityEngine;
using System.Collections;

public class WX02_015sc : MonoBehaviour {
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
		if(ManagerScript.getFieldInt(ID,player)==4 && field!=4 && !BodyScript.BurstFlag){
			int target=player;
			int f=0;
			int num=ManagerScript.getFieldAllNum(f,target);
			for(int i=0;i<3;i++){
				int x_1=ManagerScript.getFieldRankID(f,num-1-i,target);
				if(x_1>=0){
					BodyScript.effectTargetID.Add(x_1+50*target);
					BodyScript.effectMotion.Add(7);
				}
			}
			if(BodyScript.effectTargetID.Count>0)BodyScript.effectFlag=true;
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

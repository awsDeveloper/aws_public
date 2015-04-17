using UnityEngine;
using System.Collections;

public class WX01_023sc : MonoBehaviour {
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
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int opp=1-player;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,opp);
				if(x>=0)BodyScript.effectTargetID.Add(x+50*opp);
			}
			
			int num=ManagerScript.getFieldAllNum(6,opp);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(6,i,opp);
				BodyScript.effectTargetID.Add(x+50*opp);
			}
			
			if(BodyScript.effectTargetID.Count>0){
				BodyScript.effectFlag=true;
				for(int i=0;i<BodyScript.effectTargetID.Count;i++)
					BodyScript.effectMotion.Add(7);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

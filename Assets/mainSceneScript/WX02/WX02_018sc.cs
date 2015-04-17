using UnityEngine;
using System.Collections;

public class WX02_018sc : MonoBehaviour {
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
			int target=player;
			int f=0;
			int Num=ManagerScript.getFieldAllNum(f,target);
			int x=ManagerScript.getFieldRankID(f,Num-1,target);
			if(x>=0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(x+50*target);
				BodyScript.effectMotion.Add(29);
				
				int[] c=ManagerScript.getCardClass(x,target);
				if(c[0]==2 && (c[1]==0 || c[1]==1)){
					for(int i=0;i<2&&i<Num;i++){
						BodyScript.effectTargetID.Add(50*target);
						BodyScript.effectMotion.Add(2);
					}
				}
			}
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
}

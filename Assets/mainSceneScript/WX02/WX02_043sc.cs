using UnityEngine;
using System.Collections;

public class WX02_043sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool costFlag=false;
	bool bunki_1=false;
	bool bunki_2=false;

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
		if(BodyScript.Ignition){
			BodyScript.Ignition=false;
			int costNum=ManagerScript.getEnaColorNum(5,player);
			costNum+=ManagerScript.MultiEnaNum(player);
			if(costNum>=1 && ManagerScript.getIDConditionInt(ID,player)==1)
			{
				costFlag=true;
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(8);
			}
		}
		if(!BodyScript.effectFlag && costFlag){
			costFlag=false;
			int target=player;
			int f=0;
			int num=ManagerScript.getFieldAllNum(f,target);	
			int count=0;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(f,num-1-i,target);
				if(x>=0){
					if(checkClass(x,target))count++;
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			if(BodyScript.Targetable.Count>0){
				bunki_1=true;
				BodyScript.effectFlag=true;
				if(count>0){
					BodyScript.effectTargetID.Add(-2);
					BodyScript.effectMotion.Add(16);
				}
				else{
					BodyScript.effectTargetID.Add(-2);
					BodyScript.effectMotion.Add(22);
					bunki_2=true;
				}
			}
		}
		if(bunki_1 && (BodyScript.effectTargetID.Count==1 && BodyScript.effectTargetID[0]>=0 || bunki_2) ){
			bunki_1=false;
			int px=-1;
			if(!bunki_2)px=BodyScript.effectTargetID[0];
			if(checkClass(px%50,px/50) || bunki_2){
				bunki_2=false;
				int target=player;
				int f=0;
				int num=ManagerScript.getFieldAllNum(f,target);
				for(int i=0;i<3;i++){
					int x=ManagerScript.getFieldRankID(f,num-1-i,target);
					if(x>=0 && px!=x+50*target){
						BodyScript.effectTargetID.Add(x+50*target);
						BodyScript.effectMotion.Add(7);
					}
				}	
			}
			else{
				bunki_1=true;
				BodyScript.effectTargetID[0]=-2;
				BodyScript.Targetable.Add(px);
			}
		}
	}
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return c[0]==3 && c[1]==1;
	}
}

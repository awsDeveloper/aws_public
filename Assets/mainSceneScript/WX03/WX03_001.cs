using UnityEngine;
using System.Collections;

public class WX03_001 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;

	int ID=-1;
	int player=-1;
	bool costFlag=false;

	// Use this for initialization
	void Start () {
		GameObject Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();

		ID=BodyScript.ID;
		player=BodyScript.player;

		GameObject Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		int lrigDeckNum=ManagerScript.getFieldAllNum(4,player);
		
		if(ID==ManagerScript.getFieldRankID(4,lrigDeckNum-1,player)){
			if(BodyScript.Ignition){
				BodyScript.Ignition=false;
				
				int f=3;
				int target=player;
				int num=ManagerScript.getFieldAllNum(f, target);
				if(f==3)num=3;
				
				int iro=5;
				int costNum=ManagerScript.getEnaColorNum(iro,player);			
				costNum+=ManagerScript.MultiEnaNum(player);
				
				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(f,i,target);

					//コストの条件
					if(x>0 && checkClass(x,target)){
						BodyScript.Targetable.Add(x+50*target);
					}
				}
				if(BodyScript.Targetable.Count>=1 && costNum>=2){
					costFlag=true;
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(19);
					BodyScript.effectTargetID.Add(ID+player*50);
					BodyScript.effectMotion.Add(17);
					BodyScript.Cost[0]=0;
					BodyScript.Cost[1]=0;
					BodyScript.Cost[2]=0;
					BodyScript.Cost[3]=0;
					BodyScript.Cost[4]=0;
					BodyScript.Cost[5]=2;
					
					BodyScript.TargetIDEnable=true;
				}
				else {
					BodyScript.Targetable.Clear();
				}
			}
		}
		
		//after cost
		if(BodyScript.effectTargetID.Count==0 && costFlag){
			costFlag=false;
			BodyScript.Targetable.Clear();
			
			if(BodyScript.TargetID.Count==0)return;
			int lev=ManagerScript.getCardLevel(BodyScript.TargetID[0]%50,BodyScript.TargetID[0]/50);
			BodyScript.TargetID.Clear();
			
			int f=3;
			int target=1-player;
			int num=ManagerScript.getFieldAllNum(f, target);
			if(f==3)num=3;
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				
				//target check
				if(x>0 && ManagerScript.getCardLevel(x,target)==lev){
					BodyScript.Targetable.Add(x+target*50);
				}
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}
		}
	}
	
	bool checkClass(int x,int target){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,target);
		return (c[0]==3 && c[1]==1 );
	}
}

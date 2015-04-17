using UnityEngine;
using System.Collections;

public class WX02_005sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool DialogFlag=false;
	int count=1;

	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();

		BodyScript.attackArts=true;
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && !BodyScript.BurstFlag){
			int target=player;
			int f=0;
			int num=ManagerScript.getFieldAllNum(f,target);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && checkClass(x,target) && ManagerScript.getCardLevel(x,target)<=2){
					BodyScript.Targetable.Add(x+50*target);
				}
			}
			if(BodyScript.Targetable.Count>0){
				count=BodyScript.Targetable.Count;
				if(count>2)count=2;

				BodyScript.DialogFlag=true;
				BodyScript.DialogNum=1;
				BodyScript.DialogCountMax=count;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			count=int.Parse(BodyScript.messages[0]);
			BodyScript.messages.Clear();
			
			if(count>0){
				BodyScript.TargetIDEnable=true;
				BodyScript.effectFlag=true;					
				for(int i=0; i<BodyScript.Targetable.Count && i<count; i++){
					BodyScript.effectTargetID.Add(-2);
					BodyScript.effectMotion.Add(31);
				}
				BodyScript.effectTargetID.Add(50*player);
				BodyScript.effectMotion.Add(24);
			}
			else BodyScript.Targetable.Clear();
		}		
		
		if(!BodyScript.effectFlag){
			for(int i=0;i<BodyScript.TargetID.Count;i++){
				int x=BodyScript.TargetID[i];
				if(ManagerScript.getFieldInt(x%50,x/50)!=3){
					BodyScript.TargetID.RemoveAt(i);
					i--;
				}
				else if(ManagerScript.getPhaseInt()==7){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(x);
					BodyScript.effectMotion.Add(7);
					BodyScript.TargetID.RemoveAt(i);
					i--;				
				}
			}
		}
		
		field=ManagerScript.getFieldInt(ID,player);	
	}
	

	bool checkClass(int x,int cplayer){
        if (ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_アーム))
            return true;

        return ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_ウェポン);
    }
}

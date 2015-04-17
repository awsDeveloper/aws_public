using UnityEngine;
using System.Collections;

public class WX03_034 : MonoBehaviour {
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
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.powerUpValue=-5000;
	}
	
	// Update is called once per frame
	void Update () {
        if (ManagerScript.getTrashSummonID() == ID + 50 * player)
        {
            int target = 1 - player;
            int f = 3;
            int num = ManagerScript.getFieldAllNum(f, target);
            if (f == 3) num = 3;

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0)
                {
                    BodyScript.Targetable.Add(x + 50 * target);
                }
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(34);
            }
        }
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			int num=ManagerScript.getFieldAllNum(2,player);
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(2,i,player);
				if(x>=0 && checkClass(x,player))BodyScript.Targetable.Add(x+50*player);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.DialogFlag=true;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				chantFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(19);
			}
			else BodyScript.Targetable.Clear();
			BodyScript.messages.Clear();
		}
		
		if(BodyScript.effectTargetID.Count==0 && chantFlag){
			chantFlag=false;
			BodyScript.Targetable.Clear();
			
			BodyScript.effectTargetID.Add(player*50);
			BodyScript.effectMotion.Add(22);
			BodyScript.effectTargetID.Add(player*50);
			BodyScript.effectMotion.Add(2);
			BodyScript.effectTargetID.Add(player*50);
			BodyScript.effectMotion.Add(2);
		}
		
		//update
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	bool checkClass(int x,int target){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,target);
		return (c[0]==3 && c[1]==1 );
	}
}

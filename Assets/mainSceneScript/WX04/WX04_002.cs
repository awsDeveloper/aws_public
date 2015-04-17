using UnityEngine;
using System.Collections;

public class WX04_002 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool costFlag=false;
	
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
		//up requirement
		if(ID==ManagerScript.getLrigID(player) && ManagerScript.getFieldAllNum(6,1-player)<=4){
            int target = player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            //check exist in upList
            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0 && !ManagerScript.checkChanListExist(x, target, ID, player))
                {
                    //requirement add upList
                    if (true)
                        ManagerScript.alwaysChagePower(x, target, BodyScript.powerUpValue, ID, player);
                }
            }
        }
        else
            ManagerScript.powChanListChangerClear(ID, player);

		
		if(BodyScript.Ignition){
			BodyScript.Ignition=false;
			
			int target=player;
			int f=6;
			int num=ManagerScript.getNumForCard(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardColor(x,target)==2){
					BodyScript.effectTargetID.Add(x+50*target);
					BodyScript.effectMotion.Add(7);
				}
			}
			
			if(BodyScript.effectTargetID.Count>0){
				BodyScript.effectFlag=true;			
				BodyScript.effectTargetID.Add(50*target);
				BodyScript.effectMotion.Add(20);
			}
			
			if(ManagerScript.getEnaColorNum(2,target)>=3)costFlag=true;
		}
		
		if(costFlag && BodyScript.effectTargetID.Count==0){
			costFlag=false;
			BodyScript.Targetable.Clear();
			
			BodyScript.effectTargetID.Add((1-player)*50);
			BodyScript.effectMotion.Add(22);
			BodyScript.effectTargetID.Add((1-player)*50);
			BodyScript.effectMotion.Add(51);
		}
	}
}

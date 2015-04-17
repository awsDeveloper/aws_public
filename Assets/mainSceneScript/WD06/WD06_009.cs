using UnityEngine;
using System.Collections;

public class WD06_009 : MonoBehaviour {
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
		BodyScript.powerUpValue=1000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		//check equip
		checkEquip();
		//cip
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			BodyScript.Cost[0]=0;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=1;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=0;
			
			if(ManagerScript.checkCost(ID,player) && ManagerScript.getFieldAllNum(5,player)>0)
				BodyScript.DialogFlag=true;
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				
				int f=5;
				int target=player;
				int num=ManagerScript.getNumForCard(f,target);
				
				int x=ManagerScript.getFieldRankID(f,num-1,target);
				if(x>=0){
					BodyScript.effectTargetID.Add(x+target*50);
					BodyScript.effectMotion.Add(28);
					ManagerScript.sheilaFlag=true;
				}
			}
			
			BodyScript.messages.Clear();
		}
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(2);
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(2);
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(45);
		}
		
		field=ManagerScript.getFieldInt(ID,player);
	}
	
    void checkEquip()
    {
        //check situation
        if (ManagerScript.getFieldInt(ID, player) == 3 && ManagerScript.getFieldAllNum(5, player) < ManagerScript.getFieldAllNum(5, 1 - player))
        {
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
    }    
}

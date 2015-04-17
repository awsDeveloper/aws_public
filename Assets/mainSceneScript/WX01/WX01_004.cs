using UnityEngine;
using System.Collections;

public class WX01_004 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool ignition=false;
	int signiNum=0;
	int phase=0;

	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=5000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getLrigID(player)==ID){
			ManagerScript.GrowPhaseSkip[player]=true;
		}

        //always
        if (ManagerScript.getLrigID(player) == ID && ManagerScript.getTurnPlayer() == player)
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
		
        //ignition
		if(BodyScript.Ignition && !ignition){
			int rc=ManagerScript.getEnaColorNum(2,player);
			int mNum=ManagerScript.MultiEnaNum(player);
			if(rc+mNum>=3){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+player*50);
				BodyScript.effectMotion.Add(17);
				BodyScript.Cost[0]=0;
				BodyScript.Cost[1]=0;
				BodyScript.Cost[2]=3;
				BodyScript.Cost[3]=0;
				BodyScript.Cost[4]=0;
				BodyScript.Cost[5]=0;
				ManagerScript.UnblockLevel=2;
				BodyScript.Ignition=false;
			}
		}
		field=ManagerScript.getFieldInt(ID,player);
		ignition=BodyScript.Ignition;
		signiNum=ManagerScript.getFieldAllNum(3,player);
		phase=ManagerScript.getPhaseInt(); 
	}
}

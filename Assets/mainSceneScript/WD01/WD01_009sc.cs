using UnityEngine;
using System.Collections;

public class WD01_009sc : MonoBehaviour {
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

        BodyScript.powerUpValue = 1000;
	}
	
	// Update is called once per frame
	void Update () {
        if (ManagerScript.getFieldInt(ID, player) == 3 && ManagerScript.getTurnPlayer() != player)
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
		
        //burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,1-player);
				if(x>0){
					BodyScript.Targetable.Add(x+50*(1-player));
					if(!BodyScript.effectFlag){
						BodyScript.effectFlag=true;
						BodyScript.effectTargetID.Add(-1);
						BodyScript.effectMotion.Add(16);
					}
				}
			}
		}
		field=ManagerScript.getFieldInt(ID,player);
	}
}

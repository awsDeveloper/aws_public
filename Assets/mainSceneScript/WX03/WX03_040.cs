using UnityEngine;
using System.Collections;

public class WX03_040 : MonoBehaviour {

	//WX04_059　参照

	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool upFlag=false;
	int puv=3000;//powerUpValue
		
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.powerUpValue=2000;
	}
	
	// Update is called once per frame
	void Update () {
		checkEquip();
		
		//always
		if(field==3 && upCondition()){
            if (!upFlag)
            {
                ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower + puv);
                upFlag = true;
            }
        }
        else if (upFlag)
        {
            ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower);
            upFlag = false;
        }		
		
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	void checkEquip(){		
		//check situation
		if(ManagerScript.getFieldInt(ID,player)==3){
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
                    if (ManagerScript.getCardColor(x, target) == 4)
                        ManagerScript.alwaysChagePower(x, target, BodyScript.powerUpValue, ID, player);
                }
            }
        }
        else
            ManagerScript.powChanListChangerClear(ID, player);
    }
	
	bool upCondition(){
		int target=player;
		for(int i=0;i<3;i++){
			int x=ManagerScript.getFieldRankID(3,i,target);
			if(x>=0 && ManagerScript.getCardColor(x,target)==4)return true;
		}
		
		return false;
	}
}

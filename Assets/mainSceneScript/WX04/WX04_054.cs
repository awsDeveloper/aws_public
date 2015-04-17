using UnityEngine;
using System.Collections;

public class WX04_054 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.powerUpValue=3000;

		BodyScript.MultiEnaFlag=true;
	}
	
	// Update is called once per frame
	void Update () {
		checkEquip();
		
		//ignition
		if(BodyScript.Ignition)
		{
			BodyScript.Ignition=false;

			BodyScript.Cost[0]=3;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=0;
			
			if(ManagerScript.checkCost(ID,player)){
				
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (ID + 50 * player);
				BodyScript.effectMotion.Add (17);

				int target=player;
				int f=0;
				int num=ManagerScript.getFieldAllNum(f,target);
				
				for(int i=0;i<num;i++){
					int x=ManagerScript.getFieldRankID(f,i,target);
					if(checkName(x,target)){
						BodyScript.Targetable.Add(x+50*target);
					}
				}
				
				if(BodyScript.Targetable.Count>0){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(-2);
					BodyScript.effectMotion.Add(16);
				}			
			}
		}
	}
	
    void checkEquip()
    {
        //check situation
        if (ManagerScript.getFieldInt(ID, player) == 3)
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
                    if (checkName(x,target) && ID!=x)
                        ManagerScript.alwaysChagePower(x, target, BodyScript.powerUpValue, ID, player);
                }
            }
        }
        else
            ManagerScript.powChanListChangerClear(ID, player);
    }

 	bool checkName(int x,int cplayer){
		if(x<0)return false;
		string n=ManagerScript.getCardScr(x,cplayer).Name;
		return n.Contains("サーバント");
	}
}

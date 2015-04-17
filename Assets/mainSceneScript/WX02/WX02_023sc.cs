using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX02_023sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	
	List<int> checkID=new List<int>();
	bool effectFlag=false;
	
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=2000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();

		//dialog 2
		BodyScript.checkStr.Add("ドロー");
		BodyScript.checkStr.Add("ハンデス");
		
		for(int i=0;i<BodyScript.checkStr.Count;i++){
			BodyScript.checkBox.Add(false);		
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		//check equip
		checkEquip();
		//cip
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			BodyScript.DialogFlag=true;
			BodyScript.DialogNum=0;
		}
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			effectFlag=true;
			BodyScript.DialogFlag=true;
			BodyScript.DialogNum=2;
			BodyScript.DialogCountMax=1;
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(effectFlag){
				effectFlag=false;
				
				if(BodyScript.messages[0].Contains("Yes")){
					for(int i=0;i<BodyScript.checkBox.Count;i++){
						if(BodyScript.checkBox[i])checkID.Add(i);
					}
					effect_1();
					effect_2();
				}
				else destory();				
			}
			else{
				if(BodyScript.messages[0].Contains("Yes")){
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(ID+player*50);
					BodyScript.effectMotion.Add(17);
					BodyScript.Cost[0]=0;
					BodyScript.Cost[1]=0;
					BodyScript.Cost[2]=0;
					BodyScript.Cost[3]=1;
					BodyScript.Cost[4]=0;
					BodyScript.Cost[5]=0;
					BodyScript.effectTargetID.Add(50*player);
					BodyScript.effectMotion.Add(2);					
				}
				else BodyScript.Targetable.Clear();
			}
			
			BodyScript.messages.Clear();
		}
		
		//UpDate
		field=ManagerScript.getFieldInt(ID,player);
	}
	
    void checkEquip()
    {
        //check situation
        if (ManagerScript.getFieldInt(ID, player) == 3 && ManagerScript.getFieldAllNum(2, player) >= ManagerScript.getFieldAllNum(2, 1 - player) + 2)
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
                    if (checkClass(x, target))
                        ManagerScript.alwaysChagePower(x, target, BodyScript.powerUpValue, ID, player);
                }
            }
        }
        else
            ManagerScript.powChanListChangerClear(ID, player);
    }

    bool checkClass(int x, int target)
    {
        if (x < 0) return false;

        int[] c = ManagerScript.getCardClass(x, target);
        return c[0] == 5 && c[1] == 0;
    }
	
	void destory(){
		while(checkID.Count>0){
			BodyScript.checkBox[checkID[0]]=false;
			checkID.RemoveAt(0);
		}
	}
	void effect_1(){
		if(BodyScript.checkBox[0]){
			BodyScript.effectFlag=true;
			BodyScript.effectTargetID.Add(50*player);
			BodyScript.effectMotion.Add(2);
		}
	}
	void effect_2(){
		if(BodyScript.checkBox[1]){
			int target=1-player;
			int f=2;
			int num=ManagerScript.getFieldAllNum(f,target);
			if(num>0){	
				int rand=Random.Range(0,num);
				int x=ManagerScript.getFieldRankID(f,rand,target);
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(x+50*target);
				BodyScript.effectMotion.Add(19);
			}
		}
	}
}
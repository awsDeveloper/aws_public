using UnityEngine;
using System.Collections;

public class WX02_007 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
//	bool DialogFlag=false;
	bool costFlag_1=false;
	bool costFlag_2=false;
	
	// Use this for initialization
	void Start () {
		GameObject Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		BodyScript.powerUpValue=5000;
		
		GameObject Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		//dialog
		BodyScript.checkStr.Add("バニッシュ");
		BodyScript.checkStr.Add("パンプアップ");
		
		for(int i=0;i<BodyScript.checkStr.Count;i++){
			BodyScript.checkBox.Add(false);		
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            bool[] flag = new bool[2];
            int target = 1 - player;
            int[] count = new int[2] { 0, 0 };

            //ignition check 1
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, target);
                if (x >= 0 && ManagerScript.getCardPower(x, target) <= 5000) count[0]++;
            }
            if (enaCheclk(new int[6] { 0, 0, 1, 0, 0, 0 }) && count[0] >= 1) flag[0] = true;

            //ignition check 2
            target = player;
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, target);
                if (x >= 0) count[1]++;
            }
            if (count[1] >= 1 && enaCheclk(new int[6] { 0, 0, 0, 0, 1, 0 })) flag[1] = true;

            //select root
            if (flag[0] && flag[1])
            {
                BodyScript.DialogFlag = true;
                BodyScript.DialogNum = 2;
                BodyScript.DialogCountMax = 1;
            }
            else if (flag[0])
            {
                effect_1();
            }
            else if (flag[1])
            {
                effect_2();
            }

        }
		
		//receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                if (BodyScript.checkBox[0]) effect_1();
                else if (BodyScript.checkBox[1]) effect_2();
            }

            BodyScript.messages.Clear();
        }
		
		//ignition 1 after cost
		if(BodyScript.effectTargetID.Count==0 && costFlag_1){
			costFlag_1=false;
			int target=1-player;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,target);
					if(x>=0 && ManagerScript.getCardPower(x,target)<=5000)BodyScript.Targetable.Add(x+50*target);
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}
		}
		
		//ignition 2 after cost
		if(BodyScript.effectTargetID.Count==0 && costFlag_2){
			costFlag_2=false;
			for(int i=0;i<3;i++){
				int x=ManagerScript.getFieldRankID(3,i,player);
				if(x>0){
                    BodyScript.effectFlag = true;
                    BodyScript.effectMotion.Add((int)Motions.PowerUpEndPhase);
                    BodyScript.effectTargetID.Add(x + 50 * player);
				}
			}
		}		
	}
	
	void effect_1(){
		costFlag_1=true;
		BodyScript.effectFlag=true;
		BodyScript.effectTargetID.Add(ID+player*50);
		BodyScript.effectMotion.Add(17);
		BodyScript.Cost[0]=0;
		BodyScript.Cost[1]=0;
		BodyScript.Cost[2]=1;
		BodyScript.Cost[3]=0;
		BodyScript.Cost[4]=0;
		BodyScript.Cost[5]=0;
	}
	
	void effect_2(){
		costFlag_2=true;
		BodyScript.effectFlag=true;
		BodyScript.effectTargetID.Add(ID+player*50);
		BodyScript.effectMotion.Add(17);
		BodyScript.Cost[0]=0;
		BodyScript.Cost[1]=0;
		BodyScript.Cost[2]=0;
		BodyScript.Cost[3]=0;
		BodyScript.Cost[4]=1;
		BodyScript.Cost[5]=0;
	}
	
	bool enaCheclk(int[] cost){
		if(cost.Length!=6)return false;
		int sum_cost=0;
		//0 check
		for(int i=0;i<6;i++){
			sum_cost+=cost[i];
		}
		if(sum_cost>ManagerScript.getFieldAllNum(6,player))return false;
		int multi=ManagerScript.MultiEnaNum(player);
		//1~5 check
		for(int i=1;i<6;i++){
			int num=ManagerScript.getEnaColorNum(i,player);
			if(num<cost[i]){
				multi-=cost[i]-num;
			}
		}
		return multi>=0;
	}
}

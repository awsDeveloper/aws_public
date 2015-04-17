using UnityEngine;
using System.Collections;

public class WX04_040 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool upFlag=false;
	int field=-1;
	bool bFlag=false;
	bool bounceFlag=false;
	bool cFlag=false;

	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		BodyScript.powerUpValue=3000;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		//always
		if(ManagerScript.getFieldInt(ID,player)==3 && getClassNum(player)>0){
            if (!upFlag)
            {
                ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower + BodyScript.powerUpValue);
                upFlag = true;
            }
        }
        else if (upFlag)
        {
            ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower);
            upFlag = false;
        }		

		//ignition
		if(BodyScript.Ignition)
		{
			BodyScript.Ignition=false;

			int target = player;
			int f=3;
			int num=ManagerScript.getNumForCard(f,target);
			
			bool[] flag=new bool[2];
			
			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0 && checkClass(x,target)>0)
				{
					BodyScript.Targetable.Add (x + 50 * (target));
					
					flag[checkClass(x,target)-1]=true;
				}
			}
			if (flag[0] && flag[1])
			{
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (65);
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (65);
				
				BodyScript.TargetIDEnable=true;
			}
			else 
				BodyScript.Targetable.Clear();

		}
		
		//burst
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag)
		{
			int target = player;
			int f=2;
			int num=ManagerScript.getNumForCard(f,target);

			bool[] flag=new bool[2];

			for (int i=0; i<num; i++)
			{
				int x = ManagerScript.getFieldRankID (f, i, target);
				if (x >= 0 && checkClass(x,target)>0)
				{
					BodyScript.Targetable.Add (x + 50 * (target));

					flag[checkClass(x,target)-1]=true;
				}
			}
			if (BodyScript.Targetable.Count >= 2 && flag[0] && flag[1])
			{
				BodyScript.DialogFlag=true;
			}
			else 
				BodyScript.Targetable.Clear();

		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				BodyScript.effectFlag = true;
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (19);
				BodyScript.effectTargetID.Add (-1);
				BodyScript.effectMotion.Add (19);

				BodyScript.TargetIDEnable=true;
			}
			
			BodyScript.messages.Clear();
		}		

		//target check
		if(BodyScript.TargetID.Count==2)
		{
			bool[] flag=new bool[2];

			for (int i = 0; i < 2; i++) {
				int c=checkClass(BodyScript.TargetID[i]%50,BodyScript.TargetID[i]/50);

				if(c>0)
					flag[c-1]=true;
			}

			BodyScript.TargetID.Clear();
			BodyScript.TargetIDEnable=false;
			
			if(flag[0] && flag[1])
			{
				if(BodyScript.effectMotion[0]==19)
					bFlag=true;

				if(BodyScript.effectMotion[0]==65)
					cFlag=true;
			}
			else 
			{
				BodyScript.effectTargetID.Clear();
				BodyScript.effectMotion.Clear();
			}
		}

		//after igni
		if(cFlag && BodyScript.effectTargetID.Count==0)
		{
			cFlag=false;

			afterBurst(5);
		}

		//after burst
		if(bFlag && BodyScript.effectTargetID.Count==0)
		{
			bFlag=false;

			afterBurst(16);

			if(BodyScript.effectTargetID.Count>0)
				bounceFlag=true;
		}

		//after burst 2
		if(bounceFlag && BodyScript.effectTargetID.Count==0)
		{
			bounceFlag=false;
			afterBurst(5);
		}

		//update
		field=ManagerScript.getFieldInt(ID,player);
		
	}
	
	int checkClass(int x,int cplayer){
/*		if(x<0)return 0;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		if(c[0]==1 && c[1]==0)
			return 1;
		if(c[0]==1 && c[1]==1)
			return 2;

		return 0;
        */
        if (x < 0) return 0;
        if (ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_アーム))
            return 1;

        if (ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_ウェポン))
            return 2;

        return 0;
	}
	
	int getClassNum(int target){//クラス２
		int num=0;
		for (int i = 0; i < 3; i++)
		{
			int x=ManagerScript.getFieldRankID(3,i,target);
			if(x>=0 && checkClass(x,target)==2)
				num++;
		}
		return num;
	}

	void afterBurst(int effect)
	{
		int target = 1-player;
		int f=3;
		int num=ManagerScript.getNumForCard(f,target);
		
		for (int i=0; i<num; i++)
		{
			int x = ManagerScript.getFieldRankID (f, i, target);
			if (x >= 0)
			{
				BodyScript.Targetable.Add (x + 50 * (target));
			}
		}
		
		if (BodyScript.Targetable.Count >0)
		{
			BodyScript.effectTargetID.Add (-1);
			BodyScript.effectMotion.Add (effect);
		}

	}
}

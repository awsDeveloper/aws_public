using UnityEngine;
using System.Collections;

public class WX04_061 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool upFlag=false;
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

		//cip
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3)
		{
			if(ManagerScript.getFieldAllNum(3,player)>=2)
				BodyScript.DialogFlag=true;
		}

		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				int target=player;
				int f=3;
				int num=ManagerScript.getNumForCard(f,target);
				
				for (int i = 0; i < num; i++) {
					int x=ManagerScript.getFieldRankID(f,i,target);
					
					if(x>=0 && x!=ID){
						BodyScript.Targetable.Add(x+50*target);
					}
				}
				
				if(BodyScript.Targetable.Count>0)
				{
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(57);
				}
			}

			BodyScript.messages.Clear();
		}		

		//update
		field=ManagerScript.getFieldInt(ID,player);

	}
	
	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return (c[0]==3 && c[1]==2);
	}

	int getClassNum(int target){//自分以外
		int num=0;
		for (int i = 0; i < 3; i++)
		{
			int x=ManagerScript.getFieldRankID(3,i,target);
			if(x>=0  && x!=ID && checkClass(x,target))
				num++;
		}
		return num;
	}
}

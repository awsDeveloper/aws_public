using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX03_028 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	List<int> equipList=new List<int>();

    bool upFlag = false;
		
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.powerUpValue=8000;
	}
	
	// Update is called once per frame
	void Update () {
		checkEquip();

        if(field == 3 && ManagerScript.getFieldAllNum((int)Fields.LRIGDECK,player)==0)
        {
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
		
	
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	void equip(int x,int eplayer){
		if(x<0)return;
		
		//check target
		if(checkTarget(x,eplayer)){
			ManagerScript.getCardScr(x,eplayer).Cost[0]--;//equip
			equipList.Add(x+50*eplayer);
		}
	}
	
	void dequip(int index){
		if(index>=equipList.Count)return;
		int px=equipList[index];
		ManagerScript.getCardScr(px%50,px/50).Cost[0]++;//dequip
		equipList.RemoveAt(index);
	}
	
	void checkEquip(){
		int target=player;
		int equipField=1;
		int fieldAll=ManagerScript.getFieldAllNum(equipField,player);
		
		//check situation
		if(ManagerScript.getFieldInt(ID,player)==3){
			for(int i=0;i<fieldAll;i++){
				int x=ManagerScript.getFieldRankID(equipField,i,target);
				if(!checkExist(x,target))equip(x,target);
			}			
		}
		else{
			while(equipList.Count>0){
				dequip(0);
			}
		}
		//equip target check
		if(equipList.Count>0){
			for(int i=0;i<equipList.Count;i++){
				int x=equipList[i];
				if(ManagerScript.getFieldInt(x%50,x/50)!=equipField){
					dequip(i);
					i--;
				}				
			}
		}
	}
	bool checkExist(int x,int player){
		for(int i=0;i<equipList.Count;i++){
			if(x+50*player==equipList[i])return true;
		}
		return false;
	}
	
	bool checkTarget(int x,int target){
		return x>=0 
			&& ManagerScript.getCardType(x,target)==1 
			&& ManagerScript.getCardColor(x,player)==BodyScript.CardColor
			&& ManagerScript.getCardScr(x,target).Cost[0]>=1;
	}
}

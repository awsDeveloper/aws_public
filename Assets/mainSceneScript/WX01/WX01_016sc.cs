using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX01_016sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	List<int> upList=new List<int>();
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
		int lrigDeckNum=ManagerScript.getFieldAllNum(4,player);
		if(ID==ManagerScript.getFieldRankID(4,lrigDeckNum-1,player)){
			bool flag_r=false;
			int[] x=new int[3];
			for(int i=0;i<3;i++){
				x[i]=ManagerScript.getFieldRankID(3,i,player);
				if(x[i]>=0){
					if(ManagerScript.getCardColor(x[i],player)==BodyScript.CardColor)flag_r=true;
				}
			}
			if(flag_r){
				for(int i=0;i<upList.Count;i++){
					bool flag2=false;
					for(int j=0;j<3;j++){
						if(upList[i]==x[j])flag2=true;
					}
					if(!flag2){
						ManagerScript.upCardPower(upList[i],player,-BodyScript.powerUpValue);
						upList.RemoveAt(i);
						i--;
					}
				}
				
				for(int j=0;j<3;j++){
					if(!checkExist(x[j]) && x[j]>=0 && ManagerScript.getCardColor(x[j],player)==BodyScript.CardColor){
						upList.Add(x[j]);
						ManagerScript.upCardPower(x[j],player,BodyScript.powerUpValue);
					}
				}
			}
			else if(upList.Count>0){
				while(upList.Count>0){
					ManagerScript.upCardPower(upList[0],player,-BodyScript.powerUpValue);
					upList.RemoveAt(0);
				}
			}
		}
		else if(upList.Count>0){
			while(upList.Count>0){
				ManagerScript.upCardPower(upList[0],player,-BodyScript.powerUpValue);
				upList.RemoveAt(0);
			}
		}
	}
	bool checkExist(int x){
		for(int i=0;i<upList.Count;i++){
			if(x==upList[i])return true;
		}
		return false;
	}
}

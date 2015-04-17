using UnityEngine;
using System.Collections;

public class WD05_009sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
//	string sss="";
	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ManagerScript.getFieldInt(ID,player)==3 && field!=3 && !BodyScript.BurstFlag){
			int[] deckNum=new int[2];
			deckNum[player]=ManagerScript.getFieldAllNum(0,player);
			deckNum[1-player]=ManagerScript.getFieldAllNum(0,1-player);
			
			for(int i=0;i<7;i++){
				int x_1=ManagerScript.getFieldRankID(0,deckNum[player]-1-i,player);
				if(x_1>=0){
					BodyScript.effectTargetID.Add(x_1+50*player);
					BodyScript.effectMotion.Add(7);
				}
				int x_2=ManagerScript.getFieldRankID(0,deckNum[1-player]-1-i,1-player);
				if(x_2>=0){
					BodyScript.effectTargetID.Add(x_2+50*(1-player));
					BodyScript.effectMotion.Add(7);
				}
			}
			if(BodyScript.effectTargetID.Count>0)BodyScript.effectFlag=true;
		}
		
		if(ManagerScript.getFieldInt(ID,player)==8 && field!=8 && BodyScript.BurstFlag){
			int trashNum=ManagerScript.getFieldAllNum(7,player);	
			for(int i=0;i<trashNum;i++){
				int x=ManagerScript.getFieldRankID(7,i,player);
				if(x>=0){
					BodyScript.Targetable.Add(x+50*player);
				}
			}
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-2);
				BodyScript.effectMotion.Add(16);
			}
		}
		field=ManagerScript.getFieldInt(ID,player);	
	}
/*	void OnGUI() {
		string sss="\n";
		for(int i=0;i<equipList.Count;i++){
			sss+=equipList[i]+"\n";
		}
		GUI.Label(new Rect(Screen.width-100,Screen.height/2,100,100),""+sss);
	}
	*/
}

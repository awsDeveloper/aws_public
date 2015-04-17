using UnityEngine;
using System.Collections;

public class PR_046 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool costFlag=false;
	bool costFlag_2=false;
	
	int ignitionID=-1;
	bool shortageFlag=false;

	// Use this for initialization
	void Start () {
		Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();
		
		BodyScript.checkStr.Add("FREEZE");
		BodyScript.checkStr.Add("バニッシュ");
		
		for(int i=0;i<BodyScript.checkStr.Count;i++){
			BodyScript.checkBox.Add(false);		
		}
	}
	
	// Update is called once per frame
	void Update () {
		
		//always
		if(ManagerScript.getFieldInt(ID,player)==4){
			if(ManagerScript.getIgnitionUpID()>=0 && ManagerScript.getIgnitionUpID()==ManagerScript.getLrigID(1-player)+50*(1-player)){
				ManagerScript.stopFlag=true;
				ignitionID=ManagerScript.getIgnitionUpID();
			}
			else if(shortageFlag){
				shortageFlag=false;

				CardScript cs=ManagerScript.getCardScr(ignitionID%50,ignitionID/50);
				cs.effectFlag=false;
				cs.effectTargetID.Clear();
				cs.effectMotion.Clear();
				
				ignitionID=-1;
				
			}
			else if(ignitionID>=0){
				
				int target=ignitionID/50;
				
				CardScript cs=ManagerScript.getCardScr(ignitionID%50,target);
				if(cs.effectFlag){
					bool flag=false;
					
					//エナコストがあるかテェック
					for(int k=0;k<cs.effectMotion.Count;k++){
						
						if(cs.effectMotion[k]==17){
							flag=true;
							
							cs.Cost[0]++;
							
							if(!ManagerScript.checkCost(ignitionID%50,target)){
								shortageFlag=true;
								ManagerScript.stopFlag=true;
								
								cs.effectFlag=false;
								cs.effectTargetID.Clear();
								cs.effectMotion.Clear();
							}
							else ignitionID=-1;
						}
					}
					//ないときは追加
					if(!flag){
						cs.Cost[0]=1;
						cs.Cost[1]=0;
						cs.Cost[2]=0;
						cs.Cost[3]=0;
						cs.Cost[4]=0;
						cs.Cost[5]=0;
						if(!ManagerScript.checkCost(ignitionID%50,target)){
							shortageFlag=true;
							ManagerScript.stopFlag=true;
							
							cs.effectFlag=false;
							cs.effectTargetID.Clear();
							cs.effectMotion.Clear();
						}
						else {
							cs.effectTargetID.Add(ignitionID);
							cs.effectMotion.Add(17);
							ignitionID=-1;
						}
					}
				}
			}
		}
		
		
		//cip
		if(ManagerScript.getFieldInt(ID,player)==4 && field!=4){
			
			int target=1-player;
			int f=3;
			int num=ManagerScript.getNumForCard(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0){
					BodyScript.Targetable.Add(x+50*target);					
				}
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(27);
			}
		}
		
		//ignition
		if(BodyScript.Ignition)
		{
 			BodyScript.Ignition=false;
			
			//Dialogを出すかを決めている
			
			int target=1-player;
			int f=3;
			int num=ManagerScript.getNumForCard(f,target);
			bool flag=false;
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardScr(x,target).Freeze){
					flag=true;		
				}
			}
			
			if(!flag){
				BodyScript.checkBox[0]=true;
				effect_1();
			}
			else
			{
				BodyScript.DialogFlag=true;
				BodyScript.DialogNum=2;
				BodyScript.DialogCountMax=1;
			}
		}
		
		//receive
		if(BodyScript.messages.Count>0){
			if(BodyScript.messages[0].Contains("Yes")){
				effect_1();
				effect_2();
			}
			
			BodyScript.messages.Clear();
		}
		
		//ignition after cost
		if(costFlag && BodyScript.effectTargetID.Count==0)
		{
			costFlag=false;
			BodyScript.Targetable.Clear();
			
			int target=1-player;
			int f=3;
			int num=ManagerScript.getNumForCard(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0){
					BodyScript.Targetable.Add(x+50*target);					
				}
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(27);
			}			
		}
		
		//ignition after cost_2
		if(costFlag_2 && BodyScript.effectTargetID.Count==0)
		{
			costFlag_2=false;
			BodyScript.Targetable.Clear();
			
			int target=1-player;
			int f=3;
			int num=ManagerScript.getNumForCard(f,target);
			
			for(int i=0;i<num;i++){
				int x=ManagerScript.getFieldRankID(f,i,target);
				if(x>=0 && ManagerScript.getCardScr(x,target).Freeze){
					BodyScript.Targetable.Add(x+50*target);					
				}
			}
			
			if(BodyScript.Targetable.Count>0){
				BodyScript.effectTargetID.Add(-1);
				BodyScript.effectMotion.Add(5);
			}			
		}
		
		field=ManagerScript.getFieldInt(ID,player);
	}
	
	void effect_1(){
		if(BodyScript.checkBox[0])
		{
			BodyScript.Cost[0]=2;
			BodyScript.Cost[1]=0;
			BodyScript.Cost[2]=0;
			BodyScript.Cost[3]=0;
			BodyScript.Cost[4]=0;
			BodyScript.Cost[5]=0;
			
			if(ManagerScript.checkCost(ID,player))
			{				
				int target=player;
				int f=2;
				int num=ManagerScript.getNumForCard(f,target);
				
				for(int i=0;i<num;i++)
				{
					int x=ManagerScript.getFieldRankID(f,i,target);
					
					if(x>=0 
						&& (ManagerScript.getCardColor(x,target)==1 || ManagerScript.getCardColor(x,target)==3) 
						&& ManagerScript.getCardType(x,target)==2)
					{
						BodyScript.Targetable.Add(x+50*target);					
					}
				}
				
				if(BodyScript.Targetable.Count>0)
				{
					costFlag=true;
					
					BodyScript.effectFlag=true;
					BodyScript.effectTargetID.Add(-1);
					BodyScript.effectMotion.Add(19);
					BodyScript.effectTargetID.Add(ID+player*50);
					BodyScript.effectMotion.Add(17);					
				}
			}
		}
	}
	
	void effect_2()
	{
		if(BodyScript.checkBox[1])
		{
			if(ManagerScript.getLrigConditionInt(player) == 1){
				costFlag_2=true;
				
				BodyScript.effectFlag=true;
				BodyScript.effectTargetID.Add(ID+50*player);
				BodyScript.effectMotion.Add(8);
				
			}
		}
	}
}

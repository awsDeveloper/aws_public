using UnityEngine;
using System.Collections;

public class PR_019sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool DialogFlag=false;
	int count=0;
	int handNum=0;
	bool chantFlag=false;
	
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
    void Update()
    {
        handNum = ManagerScript.getFieldAllNum(2, player);

        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(ID + 50 * player);
            BodyScript.effectMotion.Add(22);
            BodyScript.AntiCheck = true;
            chantFlag = true;
        }
        if (BodyScript.effectTargetID.Count == 0 && chantFlag)
        {
            chantFlag = false;
            if (!BodyScript.AntiCheck)
            {
                BodyScript.cursorCancel = true;
                count = handNum;
                effect();
            }
            else BodyScript.AntiCheck = false;
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            BodyScript.cursorCancel = false;

            count = 2;
            if(handNum< 2)
                count = handNum;

            effect();
        }

        //cancel return
        if (BodyScript.inputReturn >= 0)
        {
            int c = BodyScript.inputReturn;

            int hosei = 0;
            if (BodyScript.BurstFlag)
                hosei = 3;

            for (int i = 0; i < c + 1 || i < hosei; i++)
            {
                BodyScript.effectTargetID.Add(50 * player);
                BodyScript.effectMotion.Add(2);
            }

            BodyScript.inputReturn = -1;
        }

        field = ManagerScript.getFieldInt(ID, player);
    }

    void effect()
    {
        for (int i = 0; i < handNum; i++)
        {
            int x = ManagerScript.getFieldRankID(2, i, player);
            if (x >= 0) BodyScript.Targetable.Add(x + 50 * player);
        }

        if (BodyScript.Targetable.Count > 0)
        {
            BodyScript.effectFlag = true;
            for (int i = 0; i < count; i++)
            {
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(7);
            }
         }
    }
	
/*	void OnGUI() {
		if(DialogFlag){
			int sw=Screen.width;
			int sh=Screen.height;
			int size_x=sw/6;
			int size_y=size_x/2;
			int buttunSize_x=size_x*4/10;
			int buttunSize_y=buttunSize_x/3;
			Vector3 v=Camera.main.WorldToScreenPoint(transform.position);
			Rect boxRect=new Rect(v.x-size_x/2,sh-v.y-size_y/2,size_x,size_y);
			Rect buttunRect1=new Rect(
				boxRect.x+(size_x-buttunSize_x*2)/4,
				boxRect.y+size_y/2-buttunSize_y/2,
				buttunSize_x,
				buttunSize_y
				);
			Rect buttunRect2=new Rect(
				boxRect.x+size_x-(size_x-buttunSize_x*2)/4-buttunSize_x,
				buttunRect1.y,
				buttunSize_x,
				buttunSize_y
				);
			Rect buttunRect3=new Rect(
				boxRect.x+size_x/2-buttunSize_x/2,
				boxRect.y+size_y-buttunSize_y-5,
				buttunSize_x,
				buttunSize_y
				);
			GUI.Box(boxRect,"");
			if(BodyScript.BurstFlag){
				GUI.Label(boxRect,BodyScript.Name+"の効果を発動しますか？");
				if(GUI.Button(buttunRect1,"Yes")){
					ManagerScript.stopFlag=false;
					DialogFlag=false;
					BodyScript.effectFlag=true;
					count=3;
					effect();
				}
				if(GUI.Button(buttunRect2,"No")){
					ManagerScript.stopFlag=false;
					DialogFlag=false;
				}
			}
			else{
				GUI.Label(boxRect,"捨てる数 -> "+count);
				if(GUI.Button(buttunRect1,"+")){
					count++;
					if(count==handNum+1)count=0;
				}
				if(GUI.Button(buttunRect2,"-")){
					count--;
					if(count==-1)count=handNum;
				}
				if(GUI.Button(buttunRect3,"ok")){
					ManagerScript.stopFlag=false;
					DialogFlag=false;
					if(count>0){
						effect();
					}
				}
			}
		}
	}*/
}

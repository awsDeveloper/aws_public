using UnityEngine;
using System.Collections;

public class WX01_005 : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	bool costFlag=false;
	bool IgnitionFlag=false;

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
		int lrigDeckNum=ManagerScript.getFieldAllNum(4,player);
        if (BodyScript.Ignition && !IgnitionFlag)
        {
            int handNum = ManagerScript.getFieldAllNum(2, player);
            for (int i = 0; i < handNum; i++)
            {
                int x = ManagerScript.getFieldRankID(2, i, player);
                if (x > 0 && ManagerScript.getCardColor(x, player) == BodyScript.CardColor && ManagerScript.getCardType(x, player) == 2)
                {
                    BodyScript.Targetable.Add(x + 50 * player);
                }
            }
            if (BodyScript.Targetable.Count >= 1)
            {
                costFlag = true;
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(19);
            }
            else BodyScript.Ignition = false;
        }
		
		if(BodyScript.effectTargetID.Count==0 && costFlag){
			costFlag=false;
			BodyScript.Ignition=false;
			BodyScript.Targetable.Clear();
			
			int num=ManagerScript.getFieldAllNum(2,1-player);
			int rand=Random.Range(0,num);
			if(num>0){
				BodyScript.effectTargetID.Add(50*(1-player));
				BodyScript.effectMotion.Add(22);
				BodyScript.effectTargetID.Add(50*(1-player));
				BodyScript.effectMotion.Add(32);
			}
		}
		IgnitionFlag=BodyScript.Ignition;
	}
}

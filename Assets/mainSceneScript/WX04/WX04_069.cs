using UnityEngine;
using System.Collections;

public class WX04_069 : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID = -1;
	int player = -1;
	int field = -1;
	
	// Use this for initialization
	void Start ()
	{
		GameObject Body = transform.parent.gameObject;
		BodyScript = Body.GetComponent<CardScript> ();
		ID = BodyScript.ID;
		player = BodyScript.player;
		GameObject Manager = Body.GetComponent<CardScript> ().Manager;
		ManagerScript = Manager.GetComponent<DeckScript> ();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		//ignition
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            if (ManagerScript.getIDConditionInt(ID, player) == 1)
            {
                BodyScript.Cost[0] = 0;
                BodyScript.Cost[1] = 0;
                BodyScript.Cost[2] = 2;
                BodyScript.Cost[3] = 0;
                BodyScript.Cost[4] = 0;
                BodyScript.Cost[5] = 0;

                if (ManagerScript.checkCost(ID, player))
                {

                    BodyScript.effectFlag = true;
                    BodyScript.effectTargetID.Add(ID + 50 * player);
                    BodyScript.effectMotion.Add(17);
                    BodyScript.effectTargetID.Add(ID + 50 * player);
                    BodyScript.effectMotion.Add(65);

                    int target = player;
                    for (int i = 0; i < 3; i++)
                    {
                        int x = ManagerScript.getFieldRankID(3, i, target);
                        if (x >= 0 && checkClass(x, target) && x != ID)
                        {
                            BodyScript.Targetable.Add(x + 50 * (target));
                        }
                    }
                    if (BodyScript.Targetable.Count > 0)
                    {
                        BodyScript.effectFlag = true;
                        BodyScript.effectTargetID.Add(-1);
                        BodyScript.effectMotion.Add(53);
                    }
                }
            }
        }	
		//UpDate
		field = ManagerScript.getFieldInt (ID, player);
	}

	bool checkClass(int x,int cplayer){
		if(x<0)return false;
		int[] c=ManagerScript.getCardClass(x,cplayer);
		return (c[0]==1 && c[1]==1)||(c[0]==2 && (c[1]==0 || c[1]==1));
	}
}

using UnityEngine;
using System.Collections;

public class WX02_019sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
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
	void Update () {

        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            int target = player;
            int f = 5;
            int num = ManagerScript.getFieldAllNum(f, target);
            int x = ManagerScript.getFieldRankID(f, num - 1, target);
            if (x >= 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(x + 50 * target);
                BodyScript.effectMotion.Add(16);
                chantFlag = true;
            }
        }

        if (chantFlag && BodyScript.effectTargetID.Count == 0)
        {
            chantFlag = false;

            if (ManagerScript.getLastMotions(0) == 16)
            {
                int target = player;
                int f = 2;
                int num = ManagerScript.getFieldAllNum(f, target);

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);
                    if (x >= 0)
                    {
                        BodyScript.Targetable.Add(x + 50 * (target));
                    }
                }

                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.effectTargetID.Add(-1);
                    BodyScript.effectMotion.Add(3);
                    BodyScript.effectTargetID.Add(50 * player);
                    BodyScript.effectMotion.Add(43);
                }
            }
        }

		field=ManagerScript.getFieldInt(ID,player);
	}
}

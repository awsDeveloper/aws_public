using UnityEngine;
using System.Collections;

public class WD04_009sc : MonoBehaviour {
	GameObject Manager;
	DeckScript ManagerScript;
	GameObject Body;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;
	int field=-1;
	bool equipFlag=false;
	bool didflag=false;
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
        int count = 0;
        bool flag = false;

        for (int i = 0; i < 3; i++)
        {
            int x = ManagerScript.getFieldRankID(3, i, player);
            if (x >= 0 && ManagerScript.getCardPower(x, player) >= 15000)
            {
                count++;
                if (x == ID) flag = true;
            }
        }

        if (flag && count == 3 && !BodyScript.lancer)
        {
            BodyScript.lancer = true;
            equipFlag = true;
        }
        else
        {
            BodyScript.lancer = false;
            equipFlag = false;
        }

        if (equipFlag && BodyScript.AttackNow && !didflag)
        {
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, 1 - player);
                if (x >= 0)
                {
                    BodyScript.Targetable.Add(x + 50 * (1 - player));
                }
            }
            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(5);
                BodyScript.effectFlag = true;
                didflag = true;
            }
        }
        if (!BodyScript.AttackNow) didflag = false;


        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, 1 - player);
                if (x > 0 && ManagerScript.getCardPower(x, 1 - player) >= 10000)
                {
                    BodyScript.Targetable.Add(x + 50 * (1 - player));
                }
            }
            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(5);
            }
        }
        field = ManagerScript.getFieldInt(ID, player);

    }
}

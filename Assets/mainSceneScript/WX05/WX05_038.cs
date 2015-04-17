using UnityEngine;
using System.Collections;

public class WX05_038 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;

    int ID = -1;
    int player = -1;
    int field = -1;

    bool after = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();

        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();
    }
	
	// Update is called once per frame
	void Update () {
        bool hakken = false;

        for (int i = 0; i < 3; i++)
        {
            int x = ManagerScript.getFieldRankID(3, i, player);
            if (checkName(x, player))
                hakken = true;
        }

        if (hakken)
            BodyScript.changeColorCost(3, 1);
        else
            BodyScript.changeColorCost(3, 2);

        //chant
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            if (ManagerScript.getFieldAllNum(2, player) >= 4)
            {
                int target = 1 - player;
                int f = 3;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);

                    if (x >= 0)
                        BodyScript.Targetable.Add(x + 50 * target);

                }

                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.effectFlag = true;
                    BodyScript.effectTargetID.Add(-1);
                    BodyScript.effectMotion.Add(5);
                }
            }
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50*player);
            BodyScript.effectMotion.Add(2);
            after = true;
        }

        if (after && BodyScript.effectTargetID.Count == 0)
        {
            after = false;

            int target = player;
            int f = 2;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i <num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (checkName(x, player))
                {
                    BodyScript.DialogFlag = true;
                    BodyScript.DialogStr = "幻水姫　スパイラル・カーミラを公開しますか？";
                    BodyScript.DialogStrEnable = true;
                    break;
                }
            }
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0] == "Yes")
            {
                int target = player;
                int f = 2;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);
                    if (checkName(x, player))
                    {
                        BodyScript.effectTargetID.Add(x + 50 * target);
                        BodyScript.effectMotion.Add((int)Motions.Open);

                        BodyScript.effectTargetID.Add(x + 50 * target);
                        BodyScript.effectMotion.Add((int)Motions.Close);

                        BodyScript.effectTargetID.Add(50 * target);
                        BodyScript.effectMotion.Add((int)Motions.Draw);

                        break;
                    }
                }
            }

            BodyScript.messages.Clear();
        }

        //update
        field = ManagerScript.getFieldInt(ID, player);
	}

    bool checkName(int x,int target)
    {
        if (x < 0)
            return false;

        string s = ManagerScript.getCardScr(x, target).Name;

        return s == "幻水姫　スパイラル・カーミラ";
    }
}

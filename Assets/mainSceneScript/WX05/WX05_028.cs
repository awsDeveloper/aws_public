using UnityEngine;
using System.Collections;

public class WX05_028 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    int attackerID = -1;
    bool cipFlag = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.powerUpValue = 3000;
    }

    // Update is called once per frame
    void Update()
    {
        //cip
        if (ManagerScript.getFieldInt(ID, player) == 3 && field != 3 && field != (int)Fields.HAND)
        {
            BodyScript.changeColorCost(1, 2);

            if (ManagerScript.checkCost(ID, player))
            {
                targetIn();

                if (BodyScript.Targetable.Count > 0)
                {

                    BodyScript.DialogFlag = true;
                    cipFlag = true;
                }

                BodyScript.Targetable.Clear();
            }
        }

        //誘発のチェック
        if (attackerID != -1 && !ManagerScript.isEffectFlagUp())
        {
            BodyScript.changeColorCost(1, 1);

            if (ManagerScript.checkCost(ID, player))
                if (ManagerScript.getFieldInt(ID, player) == (int)Fields.SIGNIZONE && ManagerScript.getAttackFrontRank() == ManagerScript.getRank(ID, player))
                    BodyScript.DialogFlag = true;

            attackerID = -1;
        }

        //attackerID のセット
        if (ManagerScript.getFieldInt(ID, player) == 3 && ManagerScript.getAttackerID() != -1 && ManagerScript.getAttackerID() / 50 == 1 - player)
        {
            attackerID = ManagerScript.getAttackerID();
            ManagerScript.stopFlag = true;
        }


        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                if (cipFlag)
                {
                    cipFlag = false;

                    targetIn();

                    if (BodyScript.Targetable.Count > 0)
                    {
                        BodyScript.effectFlag = true;

                        BodyScript.effectTargetID.Add(ID + 50 * player);
                        BodyScript.effectMotion.Add((int)Motions.PayCost);

                        BodyScript.effectTargetID.Add(-1);
                        BodyScript.effectMotion.Add((int)Motions.GoTrash);
                    }
                }
                else
                {
                    BodyScript.effectFlag = true;

                    BodyScript.effectTargetID.Add(ID + 50 * player);
                    BodyScript.effectMotion.Add((int)Motions.PayCost);

                    BodyScript.effectTargetID.Add(ID + player * 50);
                    BodyScript.effectMotion.Add(34);
                }
            }

            BodyScript.messages.Clear();
        }

        //UpDate
        field = ManagerScript.getFieldInt(ID, player);
    }

    void targetIn()
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
    }
}

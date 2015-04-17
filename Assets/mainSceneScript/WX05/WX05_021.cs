using UnityEngine;
using System.Collections;

public class WX05_021 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool upFlag = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.powerUpValue = 4000;
    }

    // Update is called once per frame
    void Update()
    {
        //always power
        if (ManagerScript.getFieldInt(ID, player) == (int)Fields.SIGNIZONE
            && ManagerScript.getEffecterNowID() / 50 == player
            && ManagerScript.getEffectGoTrashID() != -1
            && ManagerScript.getWentTrashFrom() == (int)Fields.ENAZONE)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectMotion.Add((int)Motions.PowerUpEndPhase);
            BodyScript.effectTargetID.Add(ID + 50 * player);
        }

        //always effect
        if (ManagerScript.getFieldInt(ID, player) == (int)Fields.SIGNIZONE
            && ManagerScript.getCardPower(ID, player) >= 20000)
        {
            upFlag = true;
            BodyScript.DoubleCrash = true;

            if (ManagerScript.getAttackerID() == ID + 50 * player)
            {
                int target = 1 - player;
                for (int i = 0; i < 3; i++)
                {
                    int x = ManagerScript.getFieldRankID(3, i, target);
                    if (x >= 0)
                        BodyScript.Targetable.Add(x + 50 * (target));
                }

                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.effectFlag = true;
                    BodyScript.effectTargetID.Add(-1);
                    BodyScript.effectMotion.Add(5);
                }
            }
        }
        else if (upFlag)
        {
            upFlag = false;
            BodyScript.DoubleCrash = false;
        }

        //ignition
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            BodyScript.Cost[0] = 0;
            BodyScript.Cost[1] = 0;
            BodyScript.Cost[2] = 3;
            BodyScript.Cost[3] = 0;
            BodyScript.Cost[4] = 0;
            BodyScript.Cost[5] = 0;

            if (ManagerScript.checkCost(ID, player))
            {

                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(ID + 50 * player);
                BodyScript.effectMotion.Add(17);

                int target = 1 - player;
                for (int i = 0; i < 3; i++)
                {
                    int x = ManagerScript.getFieldRankID(3, i, target);
                    if (x >= 0 && ManagerScript.getCardPower(x, target) <= 12000)
                        BodyScript.Targetable.Add(x + 50 * (target));
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
            int x = ManagerScript.getDeckTopID(player);
            if (x >= 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(x + 50 * player);
                BodyScript.effectMotion.Add((int)Motions.Open);

                BodyScript.effectTargetID.Add(50 * player);
                BodyScript.effectMotion.Add((int)Motions.Draw);

                if (checkClass(x, player))
                {
                    BodyScript.effectTargetID.Add(50 * player);
                    BodyScript.effectMotion.Add((int)Motions.Draw);
                }
            }
        }

        //UpDate
        field = ManagerScript.getFieldInt(ID, player);
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[0] == 5 && c[1] == 3);
    }
}

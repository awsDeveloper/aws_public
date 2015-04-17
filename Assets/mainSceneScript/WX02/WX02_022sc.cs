using UnityEngine;
using System.Collections;

public class WX02_022sc : MonoBehaviour
{
    GameObject Manager;
    DeckScript ManagerScript;
    GameObject Body;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;
    bool costFlag = false;
    int powerSum = 0;

    // Use this for initialization
    void Start()
    {
        Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;
        BodyScript.powerUpValue = 2000;

        Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //check equip
        checkEquip();
        //cip
        if (ManagerScript.getFieldInt(ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
        {
            int target = player;
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, target);
                if (x >= 0)
                {
                    BodyScript.Targetable.Add(x + 50 * target);
                }
            }

            BodyScript.Cost[0] = 0;
            BodyScript.Cost[1] = 0;
            BodyScript.Cost[2] = 1;
            BodyScript.Cost[3] = 0;
            BodyScript.Cost[4] = 0;
            BodyScript.Cost[5] = 0;

            if (BodyScript.Targetable.Count > 0 && ManagerScript.checkCost(ID, player))
            {
                BodyScript.DialogFlag = true;
            }
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                BodyScript.effectFlag = true;
                costFlag = true;
                BodyScript.effectTargetID.Add(ID + player * 50);
                BodyScript.effectMotion.Add(17);
            }
            else BodyScript.Targetable.Clear();

            BodyScript.messages.Clear();
        }

        //cip after cost
        if (BodyScript.effectTargetID.Count == 0 && costFlag)
        {
            costFlag = false;
            BodyScript.TargetIDEnable = true;
            BodyScript.effectTargetID.Add(-1);
            BodyScript.effectMotion.Add((int)Motions.EnDoubleCrashEnd);
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            powerSum = 0;
            int target = 1 - player;
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, target);
                if (x >= 0 && ManagerScript.getCardPower(x, target) + powerSum <= 8000)
                {
                    BodyScript.Targetable.Add(x + 50 * target);
                }
            }
            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.TargetIDEnable = true;
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(5);
            }
        }
        if (BodyScript.TargetID.Count == 1)
        {
            int px = BodyScript.TargetID[0];
            BodyScript.TargetID.Clear();
            powerSum += ManagerScript.getCardPower(px % 50, px / 50);
            BodyScript.Targetable.Clear();
            int target = 1 - player;
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, target);
                if (x >= 0 && !checkTargetExist(x, target) && ManagerScript.getCardPower(x, target) + powerSum <= 8000)
                {
                    BodyScript.Targetable.Add(x + 50 * target);
                }
            }
            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(5);
            }
            else
            {
                BodyScript.TargetIDEnable = true;
            }
        }
        field = ManagerScript.getFieldInt(ID, player);
    }

    bool checkClass(int x, int target)
    {
        return ManagerScript.checkClass(x, target, cardClassInfo.精武_ウェポン);
    }

    void checkEquip()
    {
        //check situation
        if (ManagerScript.getFieldInt(ID, player) == 3 && ManagerScript.getFieldAllNum(5, player) <= 3)
        {
            int target = player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            //check exist in upList
            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0 && !ManagerScript.checkChanListExist(x, target, ID, player))
                {
                    //requirement add upList
                    if (checkClass(x, target))
                        ManagerScript.alwaysChagePower(x, target, BodyScript.powerUpValue, ID, player);
                }
            }
        }
        else
            ManagerScript.powChanListChangerClear(ID, player);
    }

    bool checkTargetExist(int x, int player)
    {
        for (int i = 0; i < BodyScript.effectTargetID.Count; i++)
        {
            if (x + 50 * player == BodyScript.effectTargetID[i]) return true;
        }
        return false;
    }



}
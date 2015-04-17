﻿using UnityEngine;
using System.Collections;

public class WX05_059 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;
    bool DialogFlag = false;
    const int selectMax = 1;
    bool chantFlag = false;
    bool effectFlag = false;
    bool effectFlag_2 = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.checkStr.Add(" 3000バニッシュ");
        BodyScript.checkStr.Add("10000バニッシュ");

        for (int i = 0; i < BodyScript.checkStr.Count; i++)
        {
            BodyScript.checkBox.Add(false);

        }

    }
    // Update is called once per frame
    void Update()
    {
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            BodyScript.DialogFlag = true;
            BodyScript.DialogCountMax = 2;
            BodyScript.DialogNum = 2;
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                effect_1();
                effect_2();
            }

            BodyScript.messages.Clear();
        }

        if (!BodyScript.DialogFlag)
        {
            BodyScript.Targetable.Clear();

            if (effectFlag && BodyScript.effectTargetID.Count == 0)
            {
                effectFlag = false;

                if (!BodyScript.AntiCheck)
                    effectSetting(true);
                else
                    BodyScript.AntiCheck = false;
            }

            if (effectFlag_2 && BodyScript.effectTargetID.Count == 0)
            {
                effectFlag_2 = false;

                if (!BodyScript.AntiCheck)
                    effectSetting(false);
                else
                    BodyScript.AntiCheck = false;
            }
        }

        field = ManagerScript.getFieldInt(ID, player);
    }

    void effectSetting(bool isNumber_1)
    {
        int target = 1-player;
        int f = 3;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (check(x, target, isNumber_1))
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count > 0)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(-1);
            BodyScript.effectMotion.Add((int)Motions.EnaCharge);
        }
    }

    bool check(int x, int target, bool isNumber_1)
    {
        if (x < 0 || ManagerScript.getCardType(x, target) != 2)
            return false;

        int powerMax = 3000;
        if (!isNumber_1)
            powerMax=10000;

        return ManagerScript.getCardPower(x, target) <= powerMax;
    }

    void effect_1()
    {
        if (!BodyScript.checkBox[0])
            return;

        BodyScript.setAntiCheck();
        effectFlag = true;
    }

    void effect_2()
    {
        if (!BodyScript.checkBox[1])
            return;

        if (ManagerScript.getLrigColor(player) != 2 || ManagerScript.getLrigLevel(player) < 4)
            return;

        BodyScript.setAntiCheck();
        effectFlag_2 = true;
    }
}

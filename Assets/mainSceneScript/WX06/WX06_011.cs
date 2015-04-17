using UnityEngine;
using System.Collections;

public class WX06_011 : MonoBehaviour {
    DeckScript ms;
    CardScript bs;
    int ID = -1;
    int player = -1;
    bool effectFlag = false;
    bool effectFlag_2 = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        bs = Body.GetComponent<CardScript>();
        ID = bs.ID;
        player = bs.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ms = Manager.GetComponent<DeckScript>();

        bs.checkStr.Add("サーチ");
        bs.checkStr.Add("サモン");

        for (int i = 0; i < bs.checkStr.Count; i++)
            bs.checkBox.Add(false);

        bs.attackArts = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (bs.isChanted())
        {
            bs.DialogFlag = true;
            bs.DialogCountMax = 1;
            bs.DialogNum = 2;
        }

        //receive
        if (bs.messages.Count > 0)
        {
            if (bs.messages[0].Contains("Yes"))
            {
                effect_1();
                effect_2();
            }

            bs.messages.Clear();
        }

        //after chant
        if (!bs.DialogFlag)
        {
            bs.Targetable.Clear();

            if (effectFlag && bs.effectTargetID.Count == 0)
            {
                effectFlag = false;

                if (!bs.AntiCheck)
                    effectSetting(true);
                else
                    bs.AntiCheck = false;
            }

            if (effectFlag_2 && bs.effectTargetID.Count == 0)
            {
                effectFlag_2 = false;

                if (!bs.AntiCheck)
                    effectSetting(false);
                else
                    bs.AntiCheck = false;
            }
        }
    }

    void effectSetting(bool isNumber_1)
    {
        if (isNumber_1)
        {
            int num = ms.getFieldAllNum(0, player);
            for (int i = 0; i < num; i++)
            {
                int x = ms.getFieldRankID(0, i, player);
                if (ms.checkClass(x,player, cardClassInfo.精像_天使))
                    bs.Targetable.Add(x + 50 * player);
            }

            if (bs.Targetable.Count > 0)
            {
                bs.effectFlag = true;
                bs.effectTargetID.Add(-2);
                bs.effectMotion.Add(16);
            }
        }
        else
        {
            int target = player;
            int f = (int)Fields.HAND;
            int num = ms.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ms.getFieldRankID(f, i, target);
                if (ms.checkClass(x, target, cardClassInfo.精像_天使))
                    bs.Targetable.Add(x + 50 * target);
            }

            if (bs.Targetable.Count == 0)
                return;

            bs.setEffect(-1,0, Motions.Summon);
        }
    }

    void effect_1()
    {
        if (!bs.checkBox[0])
            return;

        bs.setAntiCheck();
        effectFlag = true;
    }

    void effect_2()
    {
        if (!bs.checkBox[1])
            return;

        bs.setAntiCheck();
        effectFlag_2 = true;
    }
}
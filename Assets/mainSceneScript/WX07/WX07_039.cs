using UnityEngine;
using System.Collections;

public class WX07_039 : MonoCard
{
    bool flag = false;
    int myFieldRank = -1;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (sc.isOnBattleField())
            myFieldRank = ms.getRank(ID, player);

        if (ms.getBanishedID() == ID + 50 * player && ms.getFieldRankID((int)Fields.SIGNIZONE, 2 - myFieldRank, 1 - player) >= 0)
        {
            sc.changeColorCost(cardColorInfo.青, 2);
            if (sc.myCheckCost())
                flag = true;
        }

        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.Targetable.Clear();

            sc.changeColorCost(cardColorInfo.青, 2);
            if (sc.myCheckCost() && ms.getFieldRankID((int)Fields.SIGNIZONE, 2 - myFieldRank, 1 - player) >= 0)
                sc.setDialogNum(DialogNumType.YesNo);
        }

        if (sc.isMessageYes())
        {
            sc.setPayCost();

            int x = ms.getFieldRankID((int)Fields.SIGNIZONE, 2 - myFieldRank, 1 - player);
            sc.setEffect(x, 1 - player, Motions.EnaCharge);
        }

        ignition();

    }

    void ignition()
    {
        if (!sc.Ignition)
            return;
        sc.Ignition = false;

        if (ms.getClassNum(player, Fields.SIGNIZONE, cardClassInfo.精羅_原子) < 3)
            return;

        sc.Ignition = true;
        if (!sc.IgnitionPayCost(cardColorInfo.青, 2))
            return;

        sc.setFieldAllEffect(player, Fields.SIGNIZONE, Motions.EnaCharge);

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.EnaCharge);

    }
}


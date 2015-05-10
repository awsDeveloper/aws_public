using UnityEngine;
using System.Collections;

public class WD10_018 : MonoCard
{

    // Use this for initialization
    void Start()
    {
        beforeStart();

    }

    // Update is called once per frame
    void Update()
    {
        chant();

        burst();

    }

    void burst()
    {
        if (!sc.isBursted())
            return;

        sc.funcTargetIn(player, Fields.MAINDECK, ms.checkHavingCross);
        sc.setEffect(-2, 0, Motions.GoHand);
    }

    void chant()
    {
        if (!sc.isChanted())
            return;

        if (sc.isCrossOnBattleField(player))
            sc.funcTargetIn(1 - player, Fields.SIGNIZONE, check_2);
        else
            sc.funcTargetIn(1 - player, Fields.SIGNIZONE, check_1);

        sc.setEffect(-1, 0, Motions.EnaCharge);
    }

    bool check_1(int x, int target)
    {
        return ms.getCardPower(x, target) <= 3000;
    }

    bool check_2(int x, int target)
    {
        return ms.getCardPower(x, target) <= 10000;
    }
}

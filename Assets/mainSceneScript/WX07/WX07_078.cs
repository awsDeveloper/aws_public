using UnityEngine;
using System.Collections;

public class WX07_078 : MonoCard {
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (sc.isChanted())
        {
            sc.funcTargetIn(1 - player, Fields.SIGNIZONE);            
            sc.setEffect(-1, 0, Motions.PowerUpEndPhase);

            if (check())
            {
                sc.powerUpValue = -10000;
                sc.setCanUseFunc(check);
            }
            else
                sc.powerUpValue = -3000;

        }


        if (sc.isBursted())
        {
            sc.funcTargetIn(player, Fields.MAINDECK, ms.checkHavingCross);
            sc.setEffect(-2, 0, Motions.GoHand);
        }

    }

    bool check()
    {
        return sc.isCrossOnBattleField(player);
    }
}


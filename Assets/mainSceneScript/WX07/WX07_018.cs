using UnityEngine;
using System.Collections;

public class WX07_018 : MonoCard {


    // Use this for initialization
    void Start()
    {
        sc.attackArts = true;

    }

    // Update is called once per frame
    void Update()
    {
        sc.useLimit = !sc.isCrossOnBattleField(player);

        if (sc.isChanted())
            sc.setFuncEffect(-1, Motions.EnaCharge, 1 - player, Fields.SIGNIZONE, check);
    }

    bool check(int x, int target)
    {
        return ms.getCardPower(x, target) >= 10000;
    }
}


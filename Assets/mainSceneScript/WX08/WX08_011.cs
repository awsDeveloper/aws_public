using UnityEngine;
using System.Collections;

public class WX08_011 : MonoCard {

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
            sc.setFuncEffect(-1, Motions.EnaCharge, 1 - player, Fields.SIGNIZONE, ms.checkCross);
    }
}


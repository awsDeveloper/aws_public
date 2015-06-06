using UnityEngine;
using System.Collections;

public class WX07_020 : MonoCard {


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
        {
            sc.powerUpValue = -10000;
            sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
        }
    }

}


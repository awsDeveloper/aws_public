using UnityEngine;
using System.Collections;

public class WX07_004 : MonoCard {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        crossCip();
        heavened();
        ignition();

    }

    void crossCip()
    {
        if (sc.CrossNotCip())
            return;

        sc.powerUpValue = 2000;
        sc.setEffect(0, player, Motions.PowerUpAllEnd);
    }

    void heavened()
    {
        if (sc.mySigniNotHeaven())
            return;

        sc.funcTargetIn(player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.EnLancerEnd);
    }

    void ignition()
    {
        if (!sc.Ignition)
            return;
        sc.Ignition = false;

        if (!sc.isCrossOnBattleField(player))
            return;


        sc.Ignition = true;
        if (!sc.IgnitionDown())
            return;

        sc.setEffect(0, player, Motions.TopEnaCharge);
        sc.setEffect(0, player, Motions.TopEnaCharge);
    }
}


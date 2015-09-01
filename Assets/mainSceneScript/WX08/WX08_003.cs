using UnityEngine;
using System.Collections;

public class WX08_003 : MonoCard {

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

        if (sc.isMessageYes())
        {
            sc.setPayCost();
            sc.funcTargetIn(player, Fields.MAINDECK);
            sc.setEffect(-2, 0, Motions.GoEnaZone);
        }
    }


    void crossCip()
    {
        if (sc.CrossNotCip())
            return;

        sc.setDialogNum(DialogNumType.YesNo, cardColorInfo.緑, 1);
    }

    void heavened()
    {
        if (sc.mySigniNotHeaven())
            return;

        sc.setEffect(0, player, Motions.TopEnaCharge);
        sc.setEffect(0, player, Motions.TopEnaCharge);
    }

    void ignition()
    {
        if (!sc.Ignition)
            return;
        sc.Ignition = false;

        if (!sc.isCrossOnBattleField(player))
            return;

        sc.Ignition = true;
        if (!sc.IgnitionDownPayCost(cardColorInfo.白, 1))
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.GoHand);
    }
}
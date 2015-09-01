using UnityEngine;
using System.Collections;

public class WX08_002 : MonoCard {

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
            sc.setEffect(0, player, Motions.Draw);
        }
    }


    void crossCip()
    {
        if (sc.CrossNotCip())
            return;

        sc.setDialogNum(DialogNumType.YesNo, cardColorInfo.青, 1);
    }

    void heavened()
    {
        if (sc.mySigniNotHeaven())
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.DownAndFreeze);
    }

    void ignition()
    {
        if (!sc.Ignition)
            return;
        sc.Ignition = false;

        if (!sc.isCrossOnBattleField(player))
            return;

        sc.Ignition = true;
        if (!sc.IgnitionDownPayCost(cardColorInfo.無色, 0))//微妙かも
            return;

        sc.setEffect(0, 1 - player, Motions.oneHandDeath);
        sc.setEffect(0, 1 - player, Motions.oneHandDeath);
    }
}


using UnityEngine;
using System.Collections;

public class WX07_005 : MonoCard {

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<DialogEffTemplete>().set(EffectTemplete.triggerType.mySigniHeavened, DialogNumType.playerSelect, heavened, null);
    }

    // Update is called once per frame
    void Update()
    {
        crossCip();
//        heavened();
        ignition();

    }

    void crossCip()
    {
        if (sc.CrossNotCip())
            return;

        sc.powerUpValue = -2000;
        sc.funcTargetIn(1-player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
    }

/*    void heavened()
    {
        if (sc.mySigniNotHeaven())
            return;

        for (int i = 0; i < 5; i++)
            sc.setEffect(0, 1-player, Motions.TopGoTrash);
    }*/

    void heavened(int count)
    {
        for (int i = 0; i < 5; i++)
            sc.setEffect(0, (count + player) % 2, Motions.TopGoTrash);
    }

    void ignition()
    {
        if (!sc.Ignition)
            return;
        sc.Ignition = false;

        if (!sc.isCrossOnBattleField(player))
            return;


        sc.Ignition = true;
        if (!sc.IgnitionDownPayCost(cardColorInfo.黒, 1))
            return;

        sc.powerUpValue = -10000;
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
    }
}


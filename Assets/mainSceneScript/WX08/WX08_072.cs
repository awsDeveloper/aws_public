using UnityEngine;
using System.Collections;

public class WX08_072 : MonoCard {

    // Use this for initialization
    void Start()
    {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void chant()
    {
        sc.funcTargetIn(player, Fields.MAINDECK);
        sc.setEffect(-2, 0, Motions.GoEnaZone);
        if (!sc.isCrossOnBattleField(player))
            return;
        sc.setEffect(-2, 0, Motions.GoEnaZone);
    }

    void burst()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, ms.checkHavingCross);
    }
}


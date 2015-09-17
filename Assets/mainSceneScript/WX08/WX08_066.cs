using UnityEngine;
using System.Collections;

public class WX08_066 : MonoCard {

    // Use this for initialization
    void Start()
    {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant).addEffect(chant_2);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void chant()
    {
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.DownAndFreeze);
        sc.setEffect(-1, 0, Motions.DownAndFreeze);
    }

    void chant_2()
    {
        if (!sc.isCrossOnBattleField(player))
            return;
        sc.setEffect(0, player, Motions.Draw);
    }

    void burst()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, ms.checkHavingCross);
    }
}


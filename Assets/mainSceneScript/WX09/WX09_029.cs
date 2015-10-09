using UnityEngine;
using System.Collections;

public class WX09_029 : MonoCard {

    // Use this for initialization
    void Start()
    {
        var c = sc.AddEffectTemplete(cipTri, true);
        c.addEffect(cip, true);
        c.addEffect(cip_1);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool cipTri()
    {
        return sc.isCiped() && ms.getClassNum(player, Fields.SIGNIZONE, cardClassInfo.精械_電機) == 3;
    }

    void cip()
    {
        sc.setPayCost(cardColorInfo.青, 1);
    }

    void cip_1()
    {
        sc.setEffect(0, 1 - player, Motions.RandomHandDeath);
    }

    void burst()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, new checkFuncs(ms, cardClassInfo.精械_電機).check);
    }
}


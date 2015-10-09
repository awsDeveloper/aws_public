using UnityEngine;
using System.Collections;

public class WX09_035 : MonoCard {

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
        return sc.isCiped() && ms.getClassNum(player, Fields.SIGNIZONE, cardClassInfo.精武_毒牙) == 3;
    }

    void cip()
    {
        sc.setPayCost(cardColorInfo.黒, 1);
    }

    void cip_1()
    {
        sc.setFuncEffect(-1, Motions.PowerUpEndPhase, 1 - player, Fields.SIGNIZONE, null);
        sc.powerUpValue = -7000;
    }


    bool buChe(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精武_毒牙);
    }

    void burst()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, buChe);
    }

}


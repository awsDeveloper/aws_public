using UnityEngine;
using System.Collections;

public class WX09_023 : MonoCard
{

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
        return sc.isCiped() && ms.getClassNum(player, Fields.SIGNIZONE, cardClassInfo.精武_アーム) == 3;
    }

    bool cipChe(int x, int target)
    {
        return ms.checkColor(x, target, cardColorInfo.白);
    }

    void cip() {
        sc.setPayCost(cardColorInfo.白, 1);
    }

    void cip_1()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, cipChe);
    }


    bool buChe(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精武_アーム);
    }

    void burst()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, buChe);
    }

}


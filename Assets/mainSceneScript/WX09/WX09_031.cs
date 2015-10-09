using UnityEngine;
using System.Collections;

public class WX09_031 : MonoCard {
    // Use this for initialization
    void Start()
    {
        var c = sc.AddEffectTemplete(cipTri);
        c.addEffect(cip);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool cipTri()
    {
        return sc.isCiped() && ms.getClassNum(player, Fields.SIGNIZONE, cardClassInfo.精羅_植物) == 3;
    }

    bool cipChe(int x, int target)
    {
        return ms.checkColor(x, target, cardColorInfo.白);
    }

    void cip()
    {
        sc.setEffect(0, player, Motions.TopEnaCharge);
    }


    bool buChe(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精羅_植物);
    }

    void burst()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, buChe);
    }
}


using UnityEngine;
using System.Collections;

public class WX08_026 : MonoCard {

    // Use this for initialization
    void Start()
    {
        var ch = gameObject.AddComponent<EffectTemplete>();
        ch.setTrigger(EffectTemplete.triggerType.Chant);
        ch.addEffect(chant, false);
        ch.addEffect(chant_2, false);

        var bu = gameObject.AddComponent<EffectTemplete>();
        bu.setTrigger(EffectTemplete.triggerType.Burst, true);
        bu.addEffect(burst, true);
        bu.addEffect(burst_2, false);
    }

    // Update is called once per frame
    void Update()
    {
        sc.setCostDownValue(cardColorInfo.赤, getDownNum());
    }

    int getDownNum()
    {
        return ms.getClassNum(player, Fields.SIGNIZONE, cardClassInfo.鉱石または宝石) - ms.getFieldAllNum((int)Fields.LIFECLOTH,player);
    }

    void chant()
    {
        sc.maxPowerBanish(12000);
    }

    void chant_2()
    {
        sc.setEffect(0, 1-player, Motions.TopCrash);
    }

    void burst()
    {
        sc.ClassTargetIn(player, Fields.HAND, cardClassInfo.鉱石または宝石);
        sc.setEffect(-1, 0, Motions.CostHandDeath);
    }

    void burst_2()
    {
        sc.minPowerBanish(0);
    }
}


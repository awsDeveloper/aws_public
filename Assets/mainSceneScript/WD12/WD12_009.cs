using UnityEngine;
using System.Collections;

public class WD12_009 : MonoCard {

    // Use this for initialization
    void Start()
    {
        sc.AddEffectTemplete(EffectTemplete.triggerType.ThisSigniGoHandFromEnazone, triggered, false, true);

        var es = sc.AddEffectTemplete(EffectTemplete.triggerType.MySigniEffectSummon, true);
//        es.addEffect(effectSummoned_0, EffectTemplete.option.ifThen);
        es.addEffect(sc.getEffects(CardEffectType.PayCostOneGreen), EffectTemplete.option.ifThen);
        es.addEffect(effectSummoned);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void triggered()
    {
        int lev = 4;
        var cf = new checkFuncs(ms, lev, false);
        cf.setMax(lev);
        sc.setFuncEffect(-1, Motions.DownSummon, player, Fields.HAND, cf.check);
    }

    void effectSummoned_0()
    {
        sc.setPayCost(cardColorInfo.緑, 1);
    }
    void effectSummoned()
    {
        sc.minPowerBanish(15000);
    }

    void burst()
    {
        sc.minPowerBanish(10000);
    }
}


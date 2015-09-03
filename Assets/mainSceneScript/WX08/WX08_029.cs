using UnityEngine;
using System.Collections;

public class WX08_029 : MonoCard {

    // Use this for initialization
    void Start()
    {
        var com = gameObject.AddComponent<CrossBase>();
        com.upBase = 15000;

        var ig = gameObject.AddComponent<EffectTemplete>();
        ig.setTrigger(EffectTemplete.triggerType.Ignition);
        ig.addEffect(igni, true, EffectTemplete.checkType.Default);
        ig.addEffect(igni_2, false, EffectTemplete.checkType.Default);

        var he = gameObject.AddComponent<EffectTemplete>();
        he.setTrigger(EffectTemplete.triggerType.isHeavened);
        he.addEffect(heaven, false, EffectTemplete.checkType.Default);

        var bu = gameObject.AddComponent<EffectTemplete>();
        bu.setTrigger(EffectTemplete.triggerType.Burst);
        bu.addEffect(burst, false, EffectTemplete.checkType.Default);
        bu.addEffect(burst_2, false, EffectTemplete.checkType.Default);
    }

    // Update is called once per frame
    void Update()
    {


    }

    bool check(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精械_電機);
    }

    void igni()
    {
        sc.setFuncEffect(-1, Motions.CostHandDeath, player, Fields.HAND, check);
    }

    void igni_2()
    {
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.FREEZE);
    }

    void heaven()
    {
        sc.setFuncEffect(-1, Motions.EnaCharge, 1 - player, Fields.SIGNIZONE, ms.checkFreeze);
    }

    void burst()
    {
        sc.setFieldAllEffect(1 - player, Fields.SIGNIZONE, Motions.EffectDown);
    }

    void burst_2()
    {
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.FREEZE);
    }
}


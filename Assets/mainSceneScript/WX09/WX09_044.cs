using UnityEngine;
using System.Collections;

public class WX09_044 : MonoCard {
    // Use this for initialization
    void Start()
    {
        var ig = sc.AddEffectTemplete(EffectTemplete.triggerType.Ignition, igni, true);
        ig.addEffect(igni_1, true);
        ig.addEffect(igni_2);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void igni()
    {
        sc.setDown();
    }

    void igni_1()
    {
        sc.setFuncEffect(-1, Motions.CostHandDeath, player, Fields.HAND, chCls);
    }

    bool chCls(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.鉱石または宝石);
    }

    void igni_2()
    {
        sc.maxPowerBanish(3000);
    }
}


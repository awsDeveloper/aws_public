using UnityEngine;
using System.Collections;

public class WX09_055 : MonoCard {

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
        sc.setFuncEffect(-1, Motions.CostHandDeath, player, Fields.HAND, new checkFuncs(ms, cardClassInfo.精武_毒牙).check);
    }

    void igni_2()
    {
        sc.powerUpValue = -2000;
        sc.setFuncEffect(-1, Motions.PowerUpEndPhase, 1 - player, Fields.SIGNIZONE, null);
    }
}


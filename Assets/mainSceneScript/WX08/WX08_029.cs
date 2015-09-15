using UnityEngine;
using System.Collections;

public class WX08_029 : MonoCard {

    // Use this for initialization
    void Start()
    {
        var com = gameObject.AddComponent<CrossBase>();
        com.upBase = 15000;

        var ig = sc.AddEffectTemplete(EffectTemplete.triggerType.useAttackArts, igni,true);
        ig.addEffect(igni_2);

        sc.AddEffectTemplete(EffectTemplete.triggerType.isHeavened, heaven, false, true);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
     }

    // Update is called once per frame
    void Update()
    {


    }

    void igni()
    {
        sc.setPayCost(cardColorInfo.緑, 1);
    }

    void igni_2()
    {
        sc.setEnAbilityForMe(ability.resiBanish);
    }

    void heaven()
    {
        sc.funcTargetIn(player, Fields.ENAZONE);
        sc.setEffect(-1, 0, Motions.GoHand);
    }

    void burst()
    {
        sc.setEffect(0, player, Motions.NotDamageThisTurn);
    }

}


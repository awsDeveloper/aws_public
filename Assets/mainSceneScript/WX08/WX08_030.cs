using UnityEngine;
using System.Collections;

public class WX08_030 : MonoCard {

    // Use this for initialization
    void Start()
    {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);
 
        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
    }

    // Update is called once per frame
    void Update()
    {
        sc.Cost.setDownValue(cardColorInfo.緑, ms.getClassNum(player, Fields.SIGNIZONE, cardClassInfo.精像_美巧));
        sc.Cost.setDownValue(cardColorInfo.白, ms.getClassNum(player, Fields.ENAZONE, cardClassInfo.精像_美巧));
    }

    void chant()
    {
        sc.setFieldAllEffect(1 - player, Fields.SIGNIZONE, Motions.GoHand);
    }

    void burst()
    {
        sc.setEffect(0, player, Motions.TopEnaCharge);
        sc.setEffect(0, player, Motions.TopEnaCharge);
    }
}


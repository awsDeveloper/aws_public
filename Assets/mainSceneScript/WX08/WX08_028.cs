using UnityEngine;
using System.Collections;

public class WX08_028 : MonoCard {

	// Use this for initialization
	void Start () {
        var ch = gameObject.AddComponent<EffectTemplete>();
        ch.setTrigger(EffectTemplete.triggerType.Chant);
        ch.addEffect(chant, false);
        ch.addEffect(chant_2, false);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
     }
	
	// Update is called once per frame
	void Update () {
        sc.setCostDownValue(cardColorInfo.青, getDownNum());
 	}

    int getDownNum()
    {
        return ms.getClassNum(player, Fields.SIGNIZONE, cardClassInfo.精械_電機) + ms.getFreezeNum(1 - player);
    }

    void chant()
    {
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE, ms.checkFreeze);
        sc.setEffect(-1, 0, Motions.EnaCharge);
    }

    void chant_2()
    {
        sc.setEffect(0, player, Motions.Draw);
        sc.setEffect(0, player, Motions.Draw);
        sc.setEffect(0, player, Motions.Draw);
    }

    void burst()
    {
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.EffectDown);

        if (sc.isClassSigniOnBattleField(player, cardClassInfo.精械_電機))
            sc.setEffect(-3, 0, Motions.EnaCharge);
        else
            sc.setEffect(-3, 0, Motions.FREEZE);
 
    }
}


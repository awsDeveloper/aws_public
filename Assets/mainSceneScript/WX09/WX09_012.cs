using UnityEngine;
using System.Collections;

public class WX09_012 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<NameCostResona>().set("コードハート　Ｖ・Ａ・Ｃ", cardClassInfo.精械_電機);

        sc.AddEffectTemplete(tri).addEffect(alw);
	}
	
	// Update is called once per frame
	void Update () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip, false, true);
	
	}

    bool cipChe(int x, int target)
    {
        return ms.getCostSum(x, target) <= 3 && ms.checkColorType(x, target, cardColorInfo.青, cardTypeInfo.スペル);
    }

    void cip()
    {
        sc.setFuncEffect(-2, Motions.ChantSpell, player, Fields.TRASH, cipChe);
    }

    bool tri()
    {
        int x=ms.getChantSpellID();
        return sc.isOnBattleField() && x >= 0 && x/50==player && ms.checkColor(x%50, x/50, cardColorInfo.青);
    }

    void alw()
    {
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setFuncEffect(-1, Motions.Up, player, Fields.SIGNIZONE, null);
    }
}


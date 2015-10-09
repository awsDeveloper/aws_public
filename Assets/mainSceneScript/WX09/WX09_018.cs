using UnityEngine;
using System.Collections;

public class WX09_018 : MonoCard {
    int spellCount = 0;

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<FuncChangeBase>().set(alwChe, 15000);
        gameObject.AddComponent<funcSetAbility>().set(alwChe, ability.resiSigniEffect);


        sc.AddEffectTemplete(tri).addEffect(att);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Ignition, igni, true).addEffect(igni_1);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
	}
	
	// Update is called once per frame
    void Update()
    {
        if (ms.getStartedPhase() == (int)Phases.UpPhase)
            spellCount = 0;

        if (ms.getChantSpellID() >= 0 && ms.getChantSpellID() / 50 == player)
            spellCount++;

    }

    bool alwChe()
    {
        return sc.isOnBattleField() && sc.getFuncNum(new checkFuncs(ms, cardTypeInfo.スペル).check, player, Fields.TRASH) >= 5;
    }

    bool tri()
    {
        return sc.isAttacking() && spellCount >= 3;
    }

    void att()
    {
        sc.setFuncEffect(-1, Motions.EnaCharge, 1 - player, Fields.SIGNIZONE, null);
    }

    void igni()
    {
        sc.setDown();
    }

    void igni_1()
    {
        sc.addParameta_CostDown(cardColorInfo.無色, 2, false);
        sc.setEffect(0, player, Motions.CostDown);
    }

    void burst()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.TRASH, new checkFuncs(ms, cardTypeInfo.スペル).check);
        sc.setEffect(-2, 0, Motions.GoHand);
    }

}


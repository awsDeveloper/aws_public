using UnityEngine;
using System.Collections;

public class WX08_060 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant).addEffect(chant_2);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void chant()
    {
        sc.maxPowerBanish(5000);
    }

    void chant_2()
    {
        if (!sc.isCrossOnBattleField(player))
            return;
        sc.setFuncEffect(-1, Motions.EnAbility, player, Fields.SIGNIZONE, null);
        sc.addParameta(parametaKey.EnAbilityType, (int)ability.DoubleCrash);
    }

    void burst()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, ms.checkHavingCross);
    }
}


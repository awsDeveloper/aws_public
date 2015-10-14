using UnityEngine;
using System.Collections;

public class PR_216 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.encoreCost = new colorCostArry(cardColorInfo.無色, 1);
        sc.SpellCutIn = true;
        sc.notMainArts = true;
        sc.attackArts = true;

        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void chant()
    {
        sc.powerUpValue = 3000;
        sc.addParameta(parametaKey.EnAbilityType, (int)ability.YourTurnResiEff);
        sc.setFuncEffect(-1, Motions.PowerUpEndPhase, player, Fields.SIGNIZONE, null);
        sc.setFuncEffect(-3, Motions.EnAbility, player, Fields.SIGNIZONE, null);
    }
}


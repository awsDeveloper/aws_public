using UnityEngine;
using System.Collections;

public class WX08_073 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool check(int x, int target)
    {
        return ms.checkAbility(x, target, ability.Lancer);
    }

    void chant()
    {
        sc.setFuncEffect(-1, Motions.PowerUpEndPhase, player, Fields.SIGNIZONE, check);
        sc.powerUpValue = 10000;

        sc.setEffect(-3, 0, Motions.EnAbility);
        sc.addParameta(parametaKey.EnAbilityType, (int)ability.TwoChargeAfterCrash);
    }
}


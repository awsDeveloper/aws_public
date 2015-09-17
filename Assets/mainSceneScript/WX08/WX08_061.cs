using UnityEngine;
using System.Collections;

public class WX08_061 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool check(int x, int target)
    {
        return ms.checkAbility(x, target, ability.DoubleCrash);
    }

    void chant()
    {
        sc.setFuncEffect(-1, Motions.EnAbility, player, Fields.SIGNIZONE, check);
        sc.addParameta(parametaKey.EnAbilityType, (int)ability.assassin);
    }
}


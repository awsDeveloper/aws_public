using UnityEngine;
using System.Collections;

public class WD12_006 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.attackArts = true;
        sc.notMainArts = true;

        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);
	}
	
	// Update is called once per frame
	void Update () {
        sc.beforeChantDownCost(cardClassInfo.精武_遊具, cardColorInfo.緑, 2, Fields.HAND, Motions.CostHandDeath);
	
	}

    bool chantChe(int x, int target)
    {
        return ms.checkColorType(x, target, cardColorInfo.緑, cardTypeInfo.シグニ);
    }

    void chant()
    {
        sc.setFuncEffect(-1, Motions.Summon, player, Fields.ENAZONE, chantChe);
    }

}


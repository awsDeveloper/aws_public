using UnityEngine;
using System.Collections;

public class WD12_007 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.removed, removed);
        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip, false, true);
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.resonaSummon(sc.notResonaAndYugu, 3);
	
	}

    bool check(int x, int target)
    {
        return ms.checkType(x, target, cardTypeInfo.レゾナ) && ms.checkClass(x, target, cardClassInfo.精武_遊具) && ms.checkLevelLower(x, target, 3);
    }

    void removed()
    {
        sc.setFuncEffect(-2, Motions.Summon, player, Fields.LRIGDECK, check);
    }

    void cip()
    {
        sc.setFuncEffect(-1, Motions.Summon, player, Fields.ENAZONE, new checkFuncs(ms, cardTypeInfo.シグニ).check);
    }
}


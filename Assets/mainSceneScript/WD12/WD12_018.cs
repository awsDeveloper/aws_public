using UnityEngine;
using System.Collections;

public class WD12_018 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, sc.getEffects(CardEffectType.TopEnaCharge)).addEffect(burst);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void chant()
    {
        sc.setFuncEffect(-1, Motions.GoHand, player, Fields.ENAZONE, new checkFuncs(ms, cardTypeInfo.シグニ).check);

        if (sc.isResonaOnBattleField())
            sc.setEffect(-1, 0, Motions.GoHand);
    }

    void burst()
    {
        sc.setFuncEffect(-1, Motions.GoHand, player, Fields.ENAZONE, new checkFuncs(ms, cardTypeInfo.シグニ).check);        
    }
}


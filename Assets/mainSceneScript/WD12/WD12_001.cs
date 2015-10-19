using UnityEngine;
using System.Collections;

public class WD12_001 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.MyResonaCiped, sc.getEffects(CardEffectType.TopEnaCharge));


        sc.AddEffectTemplete(EffectTemplete.triggerType.Ignition, sc.getEffects(CardEffectType.DownThisCard), true).addEffect(igni);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void igni()
    {
        sc.setFuncEffect(-1, Motions.GoHand, player, Fields.ENAZONE, new checkFuncs(ms, cardClassInfo.精武_遊具).check);
    }
}


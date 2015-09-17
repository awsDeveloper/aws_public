using UnityEngine;
using System.Collections;

public class WX08_055 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip, true, true).addEffect(cip_2);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void cip()
    {
        sc.setFuncEffect(-1, Motions.CostHandDeath, player, Fields.HAND, ms.checkHavingCross);
    }

    void cip_2()
    {
        sc.maxPowerBanish(8000);
    }
}


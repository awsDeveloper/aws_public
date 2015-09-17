using UnityEngine;
using System.Collections;

public class WX08_057 : MonoCard {

	// Use this for initialization
	void Start () {
        var ig = sc.AddEffectTemplete(EffectTemplete.triggerType.Ignition, igni, true);
        ig.addEffect(igni_2, true);
        ig.addEffect(igni_3);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void igni()
    {
        sc.setDown();
    }

    void igni_2()
    {
        sc.setFuncEffect(-1, Motions.CostHandDeath, player, Fields.HAND, ms.checkHavingCross);
    }

    void igni_3()
    {
        sc.maxPowerBanish(7000);
    }

    
}


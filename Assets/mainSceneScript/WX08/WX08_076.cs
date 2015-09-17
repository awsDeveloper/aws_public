using UnityEngine;
using System.Collections;

public class WX08_076 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip, true, true).addEffect(cip_2);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void cip()
    {
        sc.ClassTargetIn(player, Fields.HAND, cardClassInfo.精生_凶蟲);
        sc.setEffect(-1, 0, Motions.CostHandDeath);
    }

    void cip_2()
    {
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE, ms.havingCharm);
        sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
        sc.powerUpValue = -10000;
    }

    void burst()
    {
        sc.setEffect(0, player, Motions.TopEnaCharge);
    }
}


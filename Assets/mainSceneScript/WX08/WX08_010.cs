using UnityEngine;
using System.Collections;

public class WX08_010 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant).addEffect(chant_2, false, EffectTemplete.checkType.True);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void chant()
    {
        sc.setEffect(0, player, Motions.NotBurstTopCrash);
        sc.setEffect(0, player, Motions.NotBurstTopCrash);
    }

    void chant_2()
    {
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        for (int i = 0; i < 2; i++)
        {
            if (ms.getEffectExecutedID(ID, player, Motions.NotBurstTopCrash) > 0)
                sc.setEffect(-1, 0, Motions.EnaCharge);
        }
    }
}


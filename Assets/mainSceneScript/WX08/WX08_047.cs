using UnityEngine;
using System.Collections;

public class WX08_047 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Ignition, igni, true).addEffect(igni_2);
	
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
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE, ms.havingCharm);
        sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
        sc.powerUpValue = -8000;
    }

}


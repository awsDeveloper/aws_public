using UnityEngine;
using System.Collections;

public class WX08_021 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip);
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.resonaSummon(sc.notResonaAndKyotyu, 2, false, Fields.HAND);
	
	}

    void cip()
    {
        sc.setEffect(0, 1 - player, Motions.SigniZonePowerUp);
        sc.powerUpValue = -7000;
    }
}


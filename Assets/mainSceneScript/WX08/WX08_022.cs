using UnityEngine;
using System.Collections;

public class WX08_022 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.funcTargetIn(player, Fields.HAND);
            if (sc.Targetable.Count > 0)
            {
                sc.setEffect(-1, 0, Motions.HandDeath);
                sc.setEffect(0, player, Motions.TopEnaCharge);
                sc.setEffect(0, player, Motions.TopEnaCharge);
            }
        }
	
	}
}


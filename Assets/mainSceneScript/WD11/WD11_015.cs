using UnityEngine;
using System.Collections;

public class WD11_015 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isRemovedThisSigni())
        {
            sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
            sc.powerUpValue = -1000;
        }
    }
}


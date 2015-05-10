using UnityEngine;
using System.Collections;

public class WD10_011 : MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();
	
	}
	
	// Update is called once per frame
    void Update()
    {

        if (!sc.isCiped() || !sc.isCrossing())
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE, check);
        sc.setEffect(-1, 0, Motions.EnaCharge);

    }

    bool check(int x, int target)
    {
        return ms.getCardPower(x, target) <= 8000;
    }
}


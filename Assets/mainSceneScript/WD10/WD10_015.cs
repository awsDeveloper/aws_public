using UnityEngine;
using System.Collections;

public class WD10_015 : MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();

        var com = gameObject.AddComponent<FuncChangeBase>();
        com.baseValue = 5000;
        com.setFunc(sc.isCrossing);
	}
	
	// Update is called once per frame
	void Update () {
        trigger();

        if (sc.isBursted())
            sc.setEffect(0, player, Motions.Draw);
	}

    void trigger()
    {
        if (!sc.isCrossTriggered())
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE, check);
        sc.setEffect(-1, 0, Motions.EnaCharge);
    }

    bool check(int x,int target)
    {
        return ms.getCardPower(x, target) <= 2000;
    }
}


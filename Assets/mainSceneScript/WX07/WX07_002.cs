using UnityEngine;
using System.Collections;

public class WX07_002 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        crossCip();
        heavened();
        ignition();
	
	}

    void crossCip()
    {
        if (sc.CrossNotCip())
            return;

        sc.powerUpValue = 2000;
        sc.funcTargetIn(player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
    }

    void heavened()
    {
        if (sc.mySigniNotHeaven())
            return;

        sc.funcTargetIn(player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.EnDoubleCrashEnd);
    }

    void ignition()
    {
        if (!sc.Ignition)
            return;
        sc.Ignition = false;

        if (!sc.isCrossOnBattleField(player))
            return;


        sc.Ignition = true;
        if (!sc.IgnitionDownPayCost(cardColorInfo.赤,1))
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE,check);
        sc.setEffect(-1, 0, Motions.EnaCharge);
    }

    bool check(int x, int target)
    {
        return ms.getCardPower(x, target) <= 12000;
    }
}


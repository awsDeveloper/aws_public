using UnityEngine;
using System.Collections;

public class WD10_009 : MonoCard{

	// Use this for initialization
	void Start () {
        beforeStart();

        var com = gameObject.AddComponent<FuncChangeBase>();
        com.baseValue = 15000;
        com.setFunc(sc.isCrossing);	
	}
	
	// Update is called once per frame
	void Update () {
        heaven();
        afterHeaven();

        burst();
	}

    void heaven()
    {
        if (!sc.isHeaven())
            return;

        sc.changeColorCost(cardColorInfo.赤, 1);
        if (!sc.myCheckCost())
            return;

        sc.DialogFlag = true;
    }

    void afterHeaven()
    {
        if (!sc.isMessageYes())
            return;

        sc.setEffect(ID, player, Motions.PayCost);
        sc.setEffect(0, 1 - player, Motions.TopCrash);
    }

    void burst()
    {
        if (!sc.isBursted())
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE, checkBurst);
        sc.setEffect(-1, 0, Motions.EnaCharge);
    }

    bool checkBurst(int x, int target)
    {
        return ms.getCardPower(x, target) <= 10000;
    }
}


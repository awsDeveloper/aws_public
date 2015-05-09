using UnityEngine;
using System.Collections;

public class WD09_001 : MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();
	
	}
	
	// Update is called once per frame
	void Update () {
        trigger();
        ignition();
	
	}

    void trigger()
    {
        if (!sc.isOnBattleField())
            return;

        int eID = ms.getExitID(-1, (int)Fields.SIGNIZONE);
        if (eID < 0)
            return;

        if (!ms.checkType(eID, cardTypeInfo.レゾナ))
            return;

        sc.setEffect(0, player, Motions.TopEnaCharge);
    }

    void ignition()
    {
        if (!sc.Ignition)
            return;

        sc.Ignition = false;

        if (!sc.isUp())
            return;

        sc.setEffect(ID, player, Motions.Down);

        sc.funcTargetIn(player, Fields.MAINDECK, check);
        sc.setEffect(-2, 0, Motions.GoHand);
    }

    bool check(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精羅_宇宙);
    }
}


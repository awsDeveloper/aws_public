using UnityEngine;
using System.Collections;

public class WD10_007 : MonoCard {
    bool chanted = false;

	// Use this for initialization
	void Start () {
        beforeStart();
	
	}
	
	// Update is called once per frame
	void Update () {
        chant();
        afterChant();
	
	}

    void afterChant()
    {
        if (!chanted || sc.effectTargetID.Count>0)
            return;

        chanted = false;
        sc.Targetable.Clear();
        sc.cursorCancel = false;

        int count= sc.inputReturn;
        sc.inputReturn=-1;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        for (int i = 0; i < count; i++)
            sc.setEffect(-1, 0, Motions.EnaCharge);
    }

    void chant()
    {
        if (!sc.isChanted())
            return;

        sc.funcTargetIn(player, Fields.HAND, checkChant);

        sc.cursorCancel = true;
        for (int i = 0; i < sc.Targetable.Count && i<2; i++)
            sc.setEffect(-1, 0, Motions.HandDeath);

        chanted = true;
    }

    bool checkChant(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精武_ウェポン);
    }
}


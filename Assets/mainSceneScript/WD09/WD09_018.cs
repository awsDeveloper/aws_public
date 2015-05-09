using UnityEngine;
using System.Collections;

public class WD09_018 : MonoCard {
    bool chanted = false;

	// Use this for initialization
	void Start () {
        beforeStart();
	
	}
	
	// Update is called once per frame
	void Update () {
        chant();
        afterChant();

        burst();
	}

    void burst()
    {
        if (!sc.isBursted())
            return;

        sc.funcTargetIn(player, Fields.MAINDECK, checkBurst);
        sc.setEffect(-2, 0, Motions.GoHand);
    }

    bool checkBurst(int x, int target)
    {
        return ms.checkColorType(x, target, cardColorInfo.白, cardTypeInfo.シグニ);
    }

    void chant()
    {
        if (!sc.isChanted())
            return;

        sc.setEffect(0,player, Motions.AntiCheck);
        sc.setAntiCheck();
        chanted = true;
    }

    void afterChant()
    {
        if (!chanted || sc.effectTargetID.Count > 0)
            return;

        chanted = false;

        if (sc.AntiCheck)
        {
            sc.AntiCheck = false;
            return;
        }


        if (!sc.isResonaOnBattleField(player))
            return;

        sc.funcTargetIn(player, Fields.MAINDECK, checkChant);
        sc.setEffect(-2, 0, Motions.GoHand);
        sc.setEffect(-2, 0, Motions.GoHand);
    }

    bool checkChant(int x, int target)
    {
        return ms.getCardLevel(x, target) <= 3 && ms.checkColorType(x, target, cardColorInfo.白, cardTypeInfo.シグニ);
    }
}


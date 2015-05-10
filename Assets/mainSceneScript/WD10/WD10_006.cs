using UnityEngine;
using System.Collections;

public class WD10_006 :MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();

        sc.attackArts = true;
	}
	
	// Update is called once per frame
	void Update () {
        beforeChant();

        chant();
	}

    void chant()
    {
        if (!sc.isChanted())
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE, checkChant);
        sc.setEffect(-1, 0, Motions.EnaCharge);
    }

    bool checkChant(int x, int target)
    {
        return ms.getCardPower(x, target) <= 12000;
    }

    bool inputReturn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            ms.setSpellCostDown((int)cardColorInfo.赤, 2, player, 1);
            ms.setSpellCostDown((int)cardColorInfo.無色, 1, player, 1);
        }

        return true;
    }


    void beforeChant()
    {
        if (!sc.chantEffectFlag)
            return;

        sc.chantEffectFlag = false;

        sc.funcTargetIn(player, Fields.HAND, checkBefore);

        if (sc.Targetable.Count > 0)
        {
            ms.SetSystemCardFromCard(-1, Motions.HandDeath, ID, player, sc.Targetable, true, inputReturn);

            if(sc.Targetable.Count>=2)
                ms.SetSystemCardFromCard(-1, Motions.HandDeath, ID, player);
        }

        sc.Targetable.Clear();
    }

    bool checkBefore(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精武_ウェポン);
    }
}


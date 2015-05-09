using UnityEngine;
using System.Collections;

public class WD09_009 : MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();

        var com = gameObject.AddComponent<FuncChangeBase>();
        com.baseValue = 15000;
        com.setFunc(checkBase);	
	}
	
	// Update is called once per frame
	void Update () {
        sc.cipDialog(cardColorInfo.白, 1);

        cip();

        burst();	
	}

    void burst()
    {
        if (!sc.isBursted())
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.GoHand);
    }

    void cip()
    {
        if (!sc.isMessageYes())
            return;

        sc.setEffect(ID, player, Motions.PayCost);

        sc.funcTargetIn(player, Fields.MAINDECK, check_cip);
        sc.setEffect(-2, 0, Motions.Summon);
    }

    bool check_cip(int x, int target)
    {
        return ms.checkColorType(x, target, cardColorInfo.白, cardTypeInfo.シグニ) && ms.getCardLevel(x, target) <= 2;
    }

    bool checkBase()
    {
        int target = player;
        int f = (int)Fields.SIGNIZONE;
        int num = ms.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (ms.checkType(x, target, cardTypeInfo.レゾナ))
                return true;
        }

        return false;
    }
}


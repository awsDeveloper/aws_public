using UnityEngine;
using System.Collections;

public class WD09_011 :MonoCard{

	// Use this for initialization
	void Start () {
        beforeStart();

	
	}
	
	// Update is called once per frame
	void Update () {
        sc.cipDialog(cardColorInfo.白, 1);

        cip();

        burst();
	}

    void cip()
    {
        if (!sc.isMessageYes())
            return;

        sc.setEffect(ID, player, Motions.PayCost);

        sc.funcTargetIn(player, Fields.MAINDECK, check_cip);
        sc.setEffect(-2, 0, Motions.GoHand);
    }

    bool check_cip(int x, int target)
    {
        return ms.checkName(x, target, "羅星　ベガ");
    }

    void burst()
    {
        if (!sc.isBursted())
            return;

        sc.setEffect(0, player, Motions.Draw);
    }
}


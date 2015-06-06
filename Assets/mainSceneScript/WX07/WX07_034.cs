using UnityEngine;
using System.Collections;

public class WX07_034 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.cipDialog(cardColorInfo.白, 2);

        receive();
        burst();
	}

    void receive()
    {
        if (!sc.isMessageYes())
            return;

        sc.setEffect(ID, player, Motions.PayCost);
        sc.setFuncEffect(-1, Motions.GoHand, 1 - player, Fields.SIGNIZONE, cipCheck);
    }

    void burst()
    {
        if (!sc.isBursted())
            return;
        sc.setFuncEffect(-1, Motions.GoHand, 1 - player, Fields.SIGNIZONE, burstCheck);

    }

    bool cipCheck(int x, int target)
    {
        return ms.getCardLevel(x, target) <= 3;
    }

    bool burstCheck(int x, int target)
    {
        return ms.getCardLevel(x, target) <= 2;
    }

}


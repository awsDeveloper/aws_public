using UnityEngine;
using System.Collections;

public class WD11_011 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.cipDialog(cardColorInfo.黒, 1);

        if (sc.isMessageYes())
            sc.setPayCost();

        if (sc.PayedCostFlag)
        {
            sc.PayedCostFlag = false;
            sc.funcTargetIn(player, Fields.TRASH, check);
            sc.setEffect(-2, 0, Motions.GoHand);
        }


        if (sc.isBursted())
            sc.setEffect(0, player, Motions.Draw);
    }

    bool check(int x, int target)
    {
        return ms.checkColorType(x, target, cardColorInfo.黒, cardTypeInfo.シグニ) && !ms.checkName(x, target, "幻蟲　キアハ");
    }
}


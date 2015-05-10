using UnityEngine;
using System.Collections;

public class WD10_008 : MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!sc.isChanted())
            return;

        sc.funcTargetIn(player, Fields.MAINDECK, check);
        sc.setEffect(-2, 0, Motions.GoHand);	
	}

    bool check(int x, int target)
    {
        return ms.getCardScr(x, target).havingCross;
    }
}


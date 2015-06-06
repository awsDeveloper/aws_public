using UnityEngine;
using System.Collections;

public class WX07_066 : MonoCard {
    bool flag = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.effectSelecter = 1 - player;
            sc.funcTargetIn(1 - player, Fields.HAND);
            sc.setEffect(-1, 0, Motions.HandDeath);
            flag = true;
        }


        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.effectSelecter = player;
            sc.Targetable.Clear();

            if (sc.isCrossOnBattleField(player))
                sc.setEffect(0, player, Motions.Draw);
        }

        if (sc.isBursted())
        {
            sc.funcTargetIn(player, Fields.MAINDECK, ms.checkHavingCross);
            sc.setEffect(-2, 0, Motions.GoHand);
        }
	
	}
}


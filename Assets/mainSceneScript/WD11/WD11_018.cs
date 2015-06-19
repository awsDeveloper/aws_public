using UnityEngine;
using System.Collections;

public class WD11_018 : MonoCard {
    bool flag = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.funcTargetIn(player, Fields.TRASH, check);
            sc.setEffect(-2, 0, Motions.GoHand);
            flag = true;
        }

        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.Targetable.Clear();

            if (sc.isResonaOnBattleField())
            {
                sc.funcTargetIn(player, Fields.TRASH, check);
                sc.setEffect(-2, 0, Motions.GoHand);
            }
        }


        if (sc.isBursted())
        {
            sc.funcTargetIn(player, Fields.TRASH, check);
            sc.setEffect(-2, 0, Motions.GoHand);
        }
	}

    bool check(int x, int target)
    {
        return ms.checkColorType(x, target, cardColorInfo.黒, cardTypeInfo.シグニ);
    }
}


using UnityEngine;
using System.Collections;

public class WX07_030 : MonoCard {
    bool flag = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        sc.beforeChantClassDownDownCost(cardClassInfo.地獣または空獣, cardColorInfo.緑, 2);

        if (sc.isChanted())
        {
            sc.funcTargetIn(player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.EnLancerEnd);
            flag = true;
        }

        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.Targetable.Clear();

            sc.minPowerBanish(12000);
        }

        if (sc.isBursted() && check())
        {
            sc.setCanUseFunc(check);
            sc.setEffect(0, player, Motions.TopGoLifeCloth);
        }
	}

    bool check()
    {
        return ms.getFieldAllNum((int)Fields.LIFECLOTH,player) <= 3;
    }
}


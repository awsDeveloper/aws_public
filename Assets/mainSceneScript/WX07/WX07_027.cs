using UnityEngine;
using System.Collections;

public class WX07_027 : MonoCard {
    bool flag = false;

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<CrossBase>().upBase = 15000;	
	}
	
	// Update is called once per frame
	void Update () {

        if (sc.Ignition)
        {
            sc.Ignition = false;

            sc.funcTargetIn(player, Fields.HAND, check);
            for (int i = 0; i < sc.Targetable.Count; i++)
                sc.setEffect(-1, 0, Motions.CostHandDeath);
            sc.cursorCancel = true;
            flag = true;
        }


        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.Targetable.Clear();
            sc.cursorCancel = false;

            sc.changeColorCost(cardColorInfo.青, 3 - sc.inputReturn);
            if (sc.myCheckCost())
            {
                sc.setEffect(ID, player, Motions.PayCost);

                sc.funcTargetIn(player, Fields.SIGNIZONE);
                sc.funcTargetIn(1-player, Fields.SIGNIZONE);
                sc.setEffect(-1, 0, Motions.EnaCharge);
            }
        }


        if (sc.isHeaven())
            sc.setFuncEffect(-2, Motions.GoHand, player, Fields.TRASH, check);

        if (sc.isBursted())
        {
            sc.setEffect(0, player, Motions.ClassNumBanish);
            sc.addParameta(parametaKey.ClassNumBanishTarget, (int)cardClassInfo.精羅_原子);
        }
	}

    bool check(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精羅_原子);
    }
}


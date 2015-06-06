using UnityEngine;
using System.Collections;

public class WX07_050 : MonoCard {
    bool flag = false;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isCiped())
        {
            sc.ClassTargetIn(player, Fields.HAND, cardClassInfo.精羅_宇宙);

            if (sc.Targetable.Count > 0)
            {
                sc.Targetable.Clear();
                sc.setDialogNum(DialogNumType.YesNo);
            }
        }

        if (sc.isMessageYes())
        {
            sc.ClassTargetIn(player, Fields.HAND, cardClassInfo.精羅_宇宙);
            sc.setEffect(-1, 0, Motions.CostHandDeath);
            if (sc.effectFlag)
                flag = true;
        }

        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.Targetable.Clear();

            sc.funcTargetIn(player, Fields.LRIGDECK, check);
            sc.setEffect(-2, 0, Motions.TempResonaSummon);
        }
        

        if (sc.isBursted())
            sc.setEffect(0, player, Motions.TopEnaCharge);
	}

    bool check(int x, int target)
    {
        return ms.checkType(x, target, cardTypeInfo.レゾナ) && ms.getCardLevel(x, target) <= 3;
    }
}


using UnityEngine;
using System.Collections;

public class WX07_009 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.useResona)
        {
            sc.useResona = false;

            sc.funcTargetIn(player, Fields.SIGNIZONE,check);

            if (sc.Targetable.Count < 2)
                sc.Targetable.Clear();
            else
            {
                sc.setSystemCardFromCard(-1, Motions.CostGoTrash, 2, sc.Targetable, false, null);
                ms.SetSystemCardFromCard(ID + 50 * player, Motions.JupiterResona, ID, player);
            }
        }

        if (sc.isCiped())
            sc.setFieldAllEffect(1 - player, Fields.SIGNIZONE, Motions.LostEffectEnd);
	}

    bool check(int x, int target)
    {
        return !ms.checkType(x, target, cardTypeInfo.レゾナ) && ms.checkColor(x, target, cardColorInfo.白);
    }
}


using UnityEngine;
using System.Collections;

public class PR_208 : MonoCard {
    bool up = false;
	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);
	
	}
	
	// Update is called once per frame
	void Update () {
        if (ms.getFieldAllNum((int)Fields.LIFECLOTH, player) <= 2)
        {
            if (!sc.attackArts)
            {
                up = true;
                sc.attackArts = true;
            }
        }
        else if (up)
        {
            up = false;
            sc.attackArts = false;
        }	
	}

    void chant()
    {
        if (ms.getFieldAllNum((int)Fields.SIGNIZONE, player) != 0)
            return;

        sc.funcTargetIn(player, Fields.TRASH, new checkFuncs(ms, cardTypeInfo.シグニ).check);

        if (sc.Targetable.Count < 3)
        {
            sc.Targetable.Clear();
            return;
        }

        sc.setEffect(-2, 0, Motions.EffectLostTempSummon);
        sc.setEffect(-2, 0, Motions.EffectLostTempSummon);
        sc.setEffect(-2, 0, Motions.EffectLostTempSummon);
    }
}


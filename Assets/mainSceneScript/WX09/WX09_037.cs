using UnityEngine;
using System.Collections;

public class WX09_037 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant).addEffect(chant_1);
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.setCostDownValue(cardColorInfo.無色, getDownVal());
	

	}

    int getDownVal()
    {
        if (ms.getLrigLevel(player) == 5)
            return 2;

        return 0;
    }

    void chant()
    {
        sc.setFuncEffect(-1, Motions.GoTrash, 1 - player, Fields.SIGNIZONE, check);
    }
    bool check(int x, int target)
    {
        return ms.checkLevelLower(x, target, ms.getLrigLevel(player) - 1);
    }

    void chant_1()
    {
        sc.setFuncEffect(-2, Motions.Summon, player, Fields.TRASH, check);
    }
}


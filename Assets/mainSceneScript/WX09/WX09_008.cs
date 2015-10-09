using UnityEngine;
using System.Collections;

public class WX09_008 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip, true, true).addEffect(cip_1);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool check(int x, int target)
    {
        return ms.checkLevelLower(x,target, 3) && ms.checkClass(x,target, cardClassInfo.精像_天使);
    }

    void cip()
    {
        sc.setPayCost(cardColorInfo.白, 1);
    }

    void cip_1()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, check);
    }
}


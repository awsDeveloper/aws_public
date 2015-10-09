using UnityEngine;
using System.Collections;

public class WX09_032 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip, true, true).addEffect(cip_1);
	
	}
	
	// Update is called once per frame
	void Update () {
        //alwys
        ms.upAlwaysFlag(alwysEffs.Kosaki, player, ID, player);
	
	}

    void cip()
    {
        sc.setPayCost(cardColorInfo.緑, 1);
    }

    void cip_1()
    {
        sc.setFuncEffect(-1, Motions.GoHand, player, Fields.ENAZONE, new checkFuncs(ms, cardTypeInfo.シグニ).check);
    }
}


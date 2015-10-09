using UnityEngine;
using System.Collections;

public class WX09_011 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<NameCostResona>().set("手剣　カクマル", "手弾　アヤボン");

        var tr = sc.AddEffectTemplete(sc.isFrontSigniBanished, true);
        tr.addEffect(triggered, true);
        tr.addEffect(triggered_1);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip, true, true).addEffect(cip_1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void triggered()
    {
        sc.setPayCost(cardColorInfo.赤, 1);
    }

    void triggered_1()
    {
        sc.setEffect(ID, player, Motions.Up);
    }

    void cip()
    {
        int num = 2;
        if (ms.getLrigLevel(player) >= 4)
            num = 0;

        sc.setPayCost(cardColorInfo.赤, num);
    }

    void cip_1()
    {
        sc.setEffect(ID, player, Motions.EnAbility);
        sc.addParameta(parametaKey.EnAbilityType, (int)ability.DoubleCrash);
    }
}


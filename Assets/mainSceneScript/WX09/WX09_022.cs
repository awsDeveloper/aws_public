using UnityEngine;
using System.Collections;

public class WX09_022 : MonoCard {

	// Use this for initialization
    void Start()
    {
        gameObject.AddComponent<funcSetAbility>().set(check, ability.Lanaje);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Ignition, igni, true).addEffect(igni_1);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    bool check()
    {
        return sc.FieldContainsName(player, "エナジェ", Fields.TRASH);
    }

    void igni()
    {
        var c = new colorCostArry(cardColorInfo.白, 1);
        c.addCost(cardColorInfo.赤, 1);
        c.addCost(cardColorInfo.青, 1);
        c.addCost(cardColorInfo.緑, 1);
        c.addCost(cardColorInfo.黒, 1);
        sc.changeCost(c);

        sc.setPayCost();
    }

    void igni_1()
    {
        sc.setFuncEffect(-1, Motions.EnaCharge, 1 - player, Fields.SIGNIZONE, null);
        sc.setEffect(-1, 0, Motions.EnaCharge);
    }
}


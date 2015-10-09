using UnityEngine;
using System.Collections;

public class WX09_015 : MonoCard {

	// Use this for initialization
	void Start () {
        var at = sc.AddEffectTemplete(EffectTemplete.triggerType.attack, true);
        at.addEffect(attack, EffectTemplete.option.ifThen);
        at.addEffect(attack_1);

        var ci = sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip, true, true);
        ci.addEffect(cip_1);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool checkName(int x, int target)
    {
        return ms.checkContainsName(x, target, "剣");
    }

    bool checkAttack(int x, int target)
    {
        return checkName(x, target) && ms.checkIsUp(x, target);
    }

    void attack()
    {
        sc.setFuncEffect(-1, Motions.EnaCharge, player, Fields.SIGNIZONE, checkAttack);
    }

    void attack_1()
    {
        int x=ms.getSigniFront(ID, player);

        if (x > 0)
            sc.setEffect(x, 1 - player, Motions.GoHand);
    }

    void cip()
    {
        sc.setPayCost(cardColorInfo.白, 1);
    }

    void cip_1()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, checkName);
    }

    void burst()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, ch_bu);
    }

    bool ch_bu(int x, int t)
    {
        return ms.checkColor(x, t, cardColorInfo.白);
    }
}


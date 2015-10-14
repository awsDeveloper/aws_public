using UnityEngine;
using System.Collections;

public class PR_176 : MonoCard {

	// Use this for initialization
	void Start () {
        var c = sc.AddEffectTemplete(EffectTemplete.triggerType.Chant);
        c.addEffect(chant_0, EffectTemplete.option.ifThen);
        c.addEffect(chant_1);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void chant_0()
    {
        sc.setFuncEffect(-1, Motions.EnaCharge, player, Fields.SIGNIZONE, null);
    }

    void chant_1()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, check);
    }

    bool check(int x, int target)
    {
        return ms.checkType(x, target, cardTypeInfo.スペル) && !ms.checkName(x, target, "ＭＡＧＩＣ　ＨＡＮＤ");
    }

}


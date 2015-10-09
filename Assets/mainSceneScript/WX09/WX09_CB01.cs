using UnityEngine;
using System.Collections;

public class WX09_CB01 : MonoCard {

	// Use this for initialization
	void Start () {
        var a = sc.AddEffectTemplete(EffectTemplete.triggerType.attack, true);
        a.addEffect(attack, true);
        a.addEffect(att_0);
        a.addFuncList(1, att_1);
        a.addFuncList(1, att_2);
        a.changeCheckStr(1, new string[] { "ドロー", "バニッシュ", "ハンデス" });
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void attack()
    {
        sc.setFuncEffect(-1, Motions.CostHandDeath, player, Fields.HAND, new checkFuncs(ms, cardClassInfo.精械_電機).check);
    }

    void att_0()
    {
        sc.setEffect(0, player, Motions.Draw);
        sc.setEffect(0, player, Motions.Draw);
    }

    void att_1()
    {
        sc.setFuncEffect(-1, Motions.EnaCharge, 1 - player, Fields.SIGNIZONE, ms.checkFreeze);
    }

    void att_2()
    {
        sc.setEffect(0, 1 - player, Motions.RandomHandDeath);
    }
}


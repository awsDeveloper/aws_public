using UnityEngine;
using System.Collections;

public class WX09_CB02 : MonoCard {

	// Use this for initialization
	void Start () {
        var b= sc.AddEffectTemplete(EffectTemplete.triggerType.Banished, true);
        b.addEffect(bani, true);
        b.addEffect(bani_1);
        b.addFuncList(1, bani_1_2);
        b.changeCheckStr(1, new string[] { "サーチ", "サモン" });
	
	}
	
	// Update is called once per frame
	void Update () {
        ms.upAlwaysFlag(alwysEffs.Cheron, player, ID, player);
	
	}

    void bani()
    {
        sc.setPayCost(cardColorInfo.緑, 1);
    }

    void bani_1()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, baniChe);
    }
    void bani_1_2()
    {
        sc.setFuncEffect(-2, Motions.Summon, player, Fields.MAINDECK, baniChe);
    }

    bool baniChe(int x, int target)
    {
        return ms.checkLevelLower(x, target, 3) && ms.checkClass(x, target, cardClassInfo.精像_美巧) && !ms.checkName(x, target, "終末の回旋　チェロン");
    }
}


using UnityEngine;
using System.Collections;

public class WX09_016 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.isHeavened, hea).addEffect(hea_1);

        var bu = sc.AddEffectTemplete(EffectTemplete.triggerType.Burst);
        bu.addEffect(burst_1);
        bu.addFuncList(0, burst_2);
        bu.changeCheckStr(0, new string[] { "サーチ", "サモン" });

	}
	
	// Update is called once per frame
	void Update () {
        //alwys
        ms.upAlwaysFlag(alwysEffs.ShubNiggurath, player, ID, player);
	
	}

    void hea()
    {
        sc.setEffect(ID, player, Motions.AntiCheck);
    }
    void hea_1()
    {
        sc.setFieldAllEffect(1 - player, Fields.SIGNIZONE, Motions.GoTrash, heaChe);
    }

    bool heaChe(int x, int target)
    {
        return ms.getCardPower(x, target) <= 10000;
    }

    void burst_1()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, buChe);
    }
    void burst_2()
    {
        sc.setFuncEffect(-2, Motions.Summon, player, Fields.MAINDECK, buChe);
    }

    bool buChe(int x, int target)
    {
        return ms.checkColorType(x, target, cardColorInfo.白, cardTypeInfo.シグニ) || ms.checkColorType(x, target, cardColorInfo.黒, cardTypeInfo.シグニ);
    }
}


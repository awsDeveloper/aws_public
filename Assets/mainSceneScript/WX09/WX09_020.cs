using UnityEngine;
using System.Collections;

public class WX09_020 : MonoCard {

    // Use this for initialization
    void Start()
    {
        sc.AddEffectTemplete(EffectTemplete.triggerType.isHeavened, hea);

        var bu = sc.AddEffectTemplete(EffectTemplete.triggerType.Burst);
        bu.addEffect(burst_1);
        bu.addFuncList(0, burst_2);
        bu.changeCheckStr(0, new string[] { "サルベージ", "サモン" });

        sc.AddEffectTemplete(tri).addEffect(al);
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool tri()
    {
        int x=ms.getEffectExitID((int)Fields.TRASH, (int)Fields.MAINDECK);
        return sc.isOnBattleField() && x >= 0 && x / 50 == player;
    }

    void al()
    {
        sc.powerUpValue = -2000;
        sc.setFuncEffect(-1, Motions.PowerUpEndPhase, 1 - player, Fields.SIGNIZONE, null);
    }

    void hea()
    {
        sc.powerUpValue = -10000;
        sc.setFieldAllEffect(1 - player, Fields.SIGNIZONE, Motions.PowerUpEndPhase, null);
    }

    void burst_1()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.TRASH, buChe);
    }
    void burst_2()
    {
        sc.setFuncEffect(-2, Motions.Summon, player, Fields.TRASH, buChe);
    }

    bool buChe(int x, int target)
    {
        return ms.checkColorType(x, target, cardColorInfo.白, cardTypeInfo.シグニ) || ms.checkColorType(x, target, cardColorInfo.黒, cardTypeInfo.シグニ);
    }
}


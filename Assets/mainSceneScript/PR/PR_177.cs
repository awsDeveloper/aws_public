using UnityEngine;
using System.Collections;

public class PR_177 : MonoCard
{

    // Use this for initialization
    void Start()
    {
        var c = sc.AddEffectTemplete(EffectTemplete.triggerType.Chant);
        c.addEffect(chant_0, EffectTemplete.option.ifThen);
        c.addEffect(chant_1);
        c.addEffect(chant_2);
        c.addFuncList(2, chant_2_1);
        c.changeCheckStr(2, new string[] {"ハンド","エナ" });
        c.addEffect(chant_3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void chant_0()
    {
        sc.setFuncEffect(-1, Motions.EnaCharge, player, Fields.SIGNIZONE, null);
    }

    void chant_1()
    {
        for (int i = 0; i < 4; i++)
            sc.setEffect(0, player, Motions.TopGoShowZone);
    }

    void chant_2_base(Motions m)
    {
        sc.showZoneTargIn(new checkFuncs(ms, cardClassInfo.精像_美巧).check);
        sc.setEffect(-1, 0, m);
    }

    void chant_2()
    {
        chant_2_base(Motions.GoHand);
    }

    void chant_2_1()
    {
        chant_2_base(Motions.GoEnaZone);
    }

    void chant_3()
    {
        sc.setEffect(-1, 0, Motions.ShowZoneGoBottom);
    }
}

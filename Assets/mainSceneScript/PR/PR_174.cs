using UnityEngine;
using System.Collections;

public class PR_174 : MonoCard
{

    // Use this for initialization
    void Start()
    {
        var c0 = sc.AddEffectTemplete(EffectTemplete.triggerType.Chant);
        c0.addEffect(chant_0, EffectTemplete.option.ifThen);
        c0.addEffect(chant_0_1);

        var c1 = sc.AddEffectTemplete(EffectTemplete.triggerType.Chant);
        c1.addEffect(chant_1, EffectTemplete.option.ifThen);
        c1.addEffect(chant_1_1);

        var em = gameObject.AddComponent<EffTemManager>();
        em.setTrigger(EffectTemplete.triggerType.Chant);
        em.setTemplete("サーチ", c0);
        em.setTemplete("バウンス", c1);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void chant_0()
    {
        sc.setFuncEffect(-1, Motions.EnaCharge, player, Fields.SIGNIZONE, sc.notResona);
    }

    void chant_0_1()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, new checkFuncs(ms, cardClassInfo.精羅_宇宙).check);
    }

    void chant_1()
    {
        sc.setFuncEffect(-1, Motions.EnaCharge, player, Fields.SIGNIZONE, new checkFuncs(ms, cardTypeInfo.レゾナ).check);
    }

    void chant_1_1()
    {
        sc.setFuncEffect(-1, Motions.GoHand, 1 - player, Fields.SIGNIZONE, new checkFuncs(ms, 3).check);
    }
}


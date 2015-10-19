using UnityEngine;
using System.Collections;

public class WD12_013 : MonoCard {

    // Use this for initialization
    void Start()
    {
        var tri = sc.AddEffectTemplete(EffectTemplete.triggerType.MySigniYourSigniBanished, true);
        tri.addEffect(sc.getEffects(CardEffectType.DownThisCard), EffectTemplete.option.ifThen);
        tri.addEffect(triggered);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void triggered()
    {
        sc.setFuncEffect(-1, Motions.Summon, player, Fields.ENAZONE, new checkFuncs(ms, 2).check);
    }
}


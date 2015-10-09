using UnityEngine;
using System.Collections;

public class WX09_006 : MonoCard {

    // Use this for initialization
    void Start()
    {
        sc.attackArts = true;

        gameObject.AddComponent<FuncChangeCost>().set(costCheck, cardColorInfo.黒, 3);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);
    }

    // Update is called once per frame
    void Update()
    {


    }

    bool costCheck()
    {
        return ms.getTurnPlayer() == 1 - player;
    }

    void chant()
    {
        sc.setFuncEffect(-2, Motions.Summon, player, Fields.TRASH, new checkFuncs(ms, cardTypeInfo.シグニ).check);
    }
}


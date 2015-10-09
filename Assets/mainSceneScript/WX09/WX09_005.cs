using UnityEngine;
using System.Collections;

public class WX09_005 : MonoCard {

    // Use this for initialization
    void Start()
    {
        sc.attackArts = true;

        gameObject.AddComponent<FuncChangeCost>().set(costCheck, cardColorInfo.緑, 3);

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
        sc.setFieldAllEffect(1 - player, Fields.SIGNIZONE, Motions.EnaCharge, check);
    }

    bool check(int x, int target)
    {
        return ms.getCardPower(x, target) >= 15000;
    }
}


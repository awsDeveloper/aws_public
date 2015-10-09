using UnityEngine;
using System.Collections;

public class WX09_003 : MonoCard {

    // Use this for initialization
    void Start()
    {
        sc.attackArts = true;

        gameObject.AddComponent<FuncChangeCost>().set(costCheck, cardColorInfo.赤, 4);

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
        sc.maxPowerTargetIn(10000);
        if (sc.Targetable.Count >= 2)
        {
            sc.setEffect(-1, 0, Motions.EnaCharge);
            sc.setEffect(-1, 0, Motions.EnaCharge);
        }
        else
            sc.Targetable.Clear();
    }

    bool check(int x, int target)
    {
        return ms.getCardPower(x, target) <= 10000;
    }
}


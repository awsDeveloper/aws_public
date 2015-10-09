using UnityEngine;
using System.Collections;

public class WX09_045 : MonoCard {

    // Use this for initialization
    void Start()
    {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void chant()
    {
        int p = 8000;

        if (sc.getFuncNum(new checkFuncs(ms, cardColorInfo.赤).check, player) == 3 && sc.isShareClassExist(player))
            p = 15000;

        sc.maxPowerBanish(p);
    }
}


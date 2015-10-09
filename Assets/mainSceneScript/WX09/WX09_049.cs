using UnityEngine;
using System.Collections;

public class WX09_049 : MonoCard {

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
        sc.setEffect(0, player, Motions.Draw);
        sc.setEffect(0, player, Motions.Draw);
        sc.setEffect(0, player, Motions.Draw);

        if (sc.getFuncNum(new checkFuncs(ms, cardColorInfo.青).check, player) == 3 && sc.isShareClassExist(player))
            sc.setEffect(0, player, Motions.Draw);
    }
}


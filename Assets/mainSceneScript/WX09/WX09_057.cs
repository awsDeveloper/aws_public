using UnityEngine;
using System.Collections;

public class WX09_057 : MonoCard {

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
        var f = new checkFuncs(ms, cardTypeInfo.シグニ);
        f.setColor(cardColorInfo.黒);
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.TRASH, f.check);

        if (sc.getFuncNum(new checkFuncs(ms, cardColorInfo.黒).check, player) == 3 && sc.isShareClassExist(player))
            sc.setEffect(-2, 0, Motions.GoHand);
    }
}


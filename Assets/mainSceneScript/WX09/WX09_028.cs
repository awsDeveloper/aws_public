using UnityEngine;
using System.Collections;

public class WX09_028 : MonoCard
{

    // Use this for initialization
    void Start()
    {
        sc.AddEffectTemplete(cipTri).addEffect(cip);

        var a = sc.AddEffectTemplete(alwTri, true);
        a.addEffect(alw, true);
        a.addEffect(alw_1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool cipTri()
    {
        return sc.isCiped() && sc.FieldContainsName(player, "Ｖ・Ａ・Ｃ", Fields.TRASH);
    }

    void cip()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.TRASH, new checkFuncs(ms, cardTypeInfo.スペル).check);
    }

    bool alwTri()
    {
        int _id = ms.getEffectExitID((int)Fields.HAND, (int)Fields.TRASH);
        return sc.isOnBattleField()
            && _id >= 0 && _id / 50 == 1 - player
            && ms.checkType(ms.getEffecterNowID(), cardTypeInfo.シグニ) && ms.getEffecterNowID() / 50 == player
            ;
    }

    void alw()
    {
        sc.setPayCost(cardColorInfo.青, 1);
    }

    void alw_1()
    {
        sc.setFuncEffect(-1, Motions.EnaCharge, 1 - player, Fields.SIGNIZONE, null);
    }

}

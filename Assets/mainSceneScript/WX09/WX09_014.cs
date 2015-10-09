using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX09_014 : MonoCard {
    List<int> cipList = new List<int>();
    List<int> banishList = new List<int>();

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<NameCostResona>().set("悪魔姫　アンナ・ミラージュ", cardClassInfo.精像_悪魔);

        var ci = sc.AddEffectTemplete(cipTri);
        ci.addEffectDefault(cip, cipCheck);

        var ba = sc.AddEffectTemplete(banishTri);
        ba.addEffectDefault(banish, banishCheck);
    }

    // Update is called once per frame
    void Update()
    {
    }

    bool cipTri()
    {
        int cID = ms.getCipSigniID();
        if (cID >= 0 && sc.isOnBattleField() && ms.checkClass(cID, cardClassInfo.精像_悪魔))
        {
            cipList.Add(cID);
            return true;
        }

        return false;
    }

    void cip_base(bool isRemove = true)
    {
        int cID = cipList[0];
        if (isRemove)
            cipList.RemoveAt(0);

        sc.setFuncEffect(-1, Motions.EnaCharge, 1 - player, Fields.SIGNIZONE, new checkFuncs(ms, ms.getCardLevel(cID) - 1).check);
    }

    void cip()
    {
        cip_base();
    }

    void cipCheck()
    {
        cip_base(false);
    }

    bool banishTri()
    {
        int bID = ms.getBanishedID();
        if (bID >= 0 && (sc.isOnBattleField() || bID == ID+50*player) && ms.checkClass(bID, cardClassInfo.精像_悪魔))
        {
            banishList.Add(bID);
            return true;
        }

        return false;
    }

    void banish_base(bool isRemove = true)
    {
        int bID = banishList[0];
        if (isRemove)
            banishList.RemoveAt(0);

        var che=new checkFuncs(ms, ms.getCardLevel(bID),false);
        che.setMax(ms.getCardLevel(bID));
        sc.setFuncEffect(-1, Motions.EnaCharge, 1 - player, Fields.SIGNIZONE, che.check);
    }

    void banish()
    {
        banish_base();
    }

    void banishCheck()
    {
        banish_base(false);
    }
}


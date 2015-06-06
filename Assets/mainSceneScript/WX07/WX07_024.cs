using UnityEngine;
using System.Collections;

public class WX07_024 : MonoCard {
    bool chanted = false;

    // Use this for initialization
    void Start()
    {
        beforeStart();
    }

    // Update is called once per frame
    void Update()
    {
        beforeChant();
        chant();
        afterChant();

        burst();
    }

    void burst()
    {
        if (!sc.isBursted())
            return;

        sc.funcTargetIn(player, Fields.MAINDECK);
        sc.setEffect(-2, 0, Motions.GoHand);
    }


    void afterChant()
    {
        if (!chanted || sc.effectTargetID.Count > 0)
            return;

        chanted = false;
        sc.Targetable.Clear();

        sc.funcTargetIn(player, Fields.MAINDECK, checkBurst);
        sc.setEffect(-2, 0, Motions.GoHand);
    }

    void chant()
    {
        if (!sc.isChanted())
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.GoTrash);

        chanted = true;
    }

    bool inputReturn(int count)
    {
        for (int i = 0; i < count; i++)
            ms.setSpellCostDown((int)cardColorInfo.白, 2, player, 0);

        return true;
    }


    void beforeChant()//専用の関数あり
    {
        if (!sc.chantEffectFlag)
            return;

        sc.chantEffectFlag = false;

        sc.funcTargetIn(player, Fields.SIGNIZONE, checkBefore);
        sc.setSystemCardFromCard(-1, Motions.Down, sc.Targetable.Count, sc.Targetable, true, inputReturn);
        sc.Targetable.Clear();
    }

    bool checkBefore(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精羅_宇宙) && ms.getIDConditionInt(x, target) == (int)Conditions.Up;
    }

    bool checkBurst(int x, int target)
    {
        return ms.checkClass(x,target, cardClassInfo.精羅_宇宙);
    }
}


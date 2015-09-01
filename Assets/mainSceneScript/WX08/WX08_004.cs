using UnityEngine;
using System.Collections;

public class WX08_004 : MonoCard {
    bool igniFlag = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ignition();
        afterIgni();

        trigger();
    }

    void ignition()
    {
        if (!sc.Ignition)
            return;
        sc.Ignition = false;

        sc.setFuncEffect(-1, Motions.GOLrigTrash, player, Fields.SIGNIZONE, checkIgni);
        if(sc.Targetable.Count > 0)
            igniFlag = true;
    }

    void afterIgni()
    {
        if (!igniFlag || sc.effectTargetID.Count > 0)
            return;

        igniFlag = false;
        sc.Targetable.Clear();

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.EnaCharge);
    }

    bool checkIgni(int x, int target)
    {
        return ms.checkType(x, target, cardTypeInfo.レゾナ);
    }

    void trigger()
    {
        if (!sc.isMyResonaCiped()) //isMyResonaCiped使ってもいいよ
            return;

        sc.powerUpValue = -7000;
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
    }
}



using UnityEngine;
using System.Collections;

public class SP06_011 : MonoCard
{
    bool upFlag = false;

    // Use this for initialization
    void Start()
    {
        beforeStart();

        FuncChangeBase f = gameObject.AddComponent<FuncChangeBase>();
        f.setFunc(func);
        f.baseValue = 15000;
    }

    // Update is called once per frame
    void Update()
    {        
        if (ms.getFieldInt(ID, player) == 3)
        {
            ms.nickelFlag[player] = true;
            upFlag = true;
        }
        else if (upFlag)
        {
            ms.nickelFlag[player] = false;
            upFlag = false;
        }

        cip();

        burst();
    }

    void burst()
    {
        if (!sc.isBursted())
            return;

        sc.setEffect(0, player, Motions.Draw);
        sc.setEffect(0, player, Motions.Draw);
        sc.setEffect(0, player, Motions.oneHandDeath);
    }

    void cip()
    {
        if (!sc.isCiped())
            return;

        sc.funcTargetIn(1 - player, Fields.TRASH, hantei);
        sc.setEffect(-2, 0, Motions.GoDeck);
    }

    bool hantei(int x,int target)
    {
        return ms.checkType(x, target, cardTypeInfo.スペル);
    }

    bool func()
    {
        return sc.getNameShurui(player, Fields.TRASH, check) >= 7;
    }

    bool check(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精羅_原子);
    }
}

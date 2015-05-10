using UnityEngine;
using System.Collections;

public class WD09_008 : MonoCard {

    // Use this for initialization
    void Start()
    {
        beforeStart();
    }

    // Update is called once per frame
    void Update()
    {
        resona();

        cip();
    }

    void resona()
    {
        if (!sc.useResona)
            return;

        sc.useResona = false;

        sc.funcTargetIn(player, Fields.SIGNIZONE, checkResona);

        if (sc.Targetable.Count < 2)
        {
            sc.Targetable.Clear();
            return;
        }

        ms.SetSystemCardFromCard(-1, Motions.GoTrash, ID, player, sc.Targetable);
        ms.SetSystemCardFromCard(-1, Motions.GoTrash, ID, player);
        sc.Targetable.Clear();

        ms.SetSystemCardFromCard(ID + 50 * player, Motions.Summon, ID, player);
    }

    bool checkResona(int x, int target)
    {
        return !ms.checkType(x, target, cardTypeInfo.レゾナ) && ms.checkClass(x, target, cardClassInfo.精羅_宇宙);
    }

    void cip()
    {
        if (!sc.isCiped())
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE,checkCip);
        sc.setEffect(-1, 0, Motions.GoHand);
    }

    bool checkCip(int x, int target)
    {
        return ms.getCardLevel(x, target) <= 2;
    }
}


using UnityEngine;
using System.Collections;

public class WD09_007 : MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();	
	}
	
	// Update is called once per frame
	void Update () {
        resona();

        cip();
	}

    void resona()
    {
        if (!sc.useResona)
            return;

        sc.useResona = false;

        sc.funcTargetIn(player, Fields.SIGNIZONE, checkResona);

        if (sc.Targetable.Count < 3){
            sc.Targetable.Clear();
            return;
        }

        for (int i = 0; i < sc.Targetable.Count; i++)
            ms.SetSystemCardFromCard(sc.Targetable[i], Motions.CostGoTrash, ID, player);
        sc.Targetable.Clear();

        ms.SetSystemCardFromCard(ID + 50 * player, Motions.Summon, ID, player);
    }

    bool checkResona(int x,int target)
    {
        return !ms.checkType(x, target, cardTypeInfo.レゾナ) && ms.checkClass(x, target, cardClassInfo.精羅_宇宙);
    }

    void cip()
    {
        if (!sc.isCiped())
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.GoTrash);
    }
}


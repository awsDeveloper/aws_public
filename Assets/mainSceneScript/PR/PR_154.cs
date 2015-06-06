using UnityEngine;
using System.Collections;

public class PR_154 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.resonaSummon(check, 2);

        if (sc.isCiped())
        {
            sc.setFuncEffect(-2, Motions.Summon, player, Fields.MAINDECK, checkCip);

            sc.setEffect(0, 1 - player, Motions.NextMinusLimit);
        }
	}

    bool checkCip(int x, int target)
    {
        return ms.getCardLevel(x, target) ==1;
    }

    bool check(int x, int target)
    {
        return !ms.checkType(x, target, cardTypeInfo.レゾナ) && ms.getCardLevel(x, target) <= 2 && ms.checkClass(x, target, cardClassInfo.精羅_宇宙);
    }
}


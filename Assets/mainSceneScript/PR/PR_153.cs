using UnityEngine;
using System.Collections;

public class PR_153 : MonoCard {

	// Use this for initialization
	void Start () {

        sc.attackArts = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.funcTargetIn(player, Fields.LRIGDECK, check);
            sc.setEffect(-2, 0, Motions.NotCipTempResona);
            if(sc.Targetable.Count>=2)
                sc.setEffect(-2, 0, Motions.NotCipTempResona);
            sc.GUIcancelEnable = true;
        }	
	}


    bool check(int x, int target)
    {
        return ms.checkType(x, target, cardTypeInfo.レゾナ);
    }
}


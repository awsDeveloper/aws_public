using UnityEngine;
using System.Collections;

public class WX07_015 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.setEffect(0, 1 - player, Motions.OpenHand);
            sc.setFuncEffect(-1, Motions.HandDeath, 1 - player, Fields.HAND, check);
            sc.setEffect(0, 1 - player, Motions.CloseHand);
        }
	
	}

    bool check(int x, int target)
    {
        return !ms.checkColor(x, target, cardColorInfo.無色);
    }
}


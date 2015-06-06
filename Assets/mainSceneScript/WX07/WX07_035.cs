using UnityEngine;
using System.Collections;

public class WX07_035 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(sc.IgnitionDown())
            sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, check);
	
	}

    bool check(int x, int target)
    {
        return ms.getCardLevel(x, target) <= 3
            && !ms.checkName(x, target, "羅星　ミモザ")
            && ms.checkClass(x, target, cardClassInfo.精羅_宇宙);
    }
}


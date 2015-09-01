using UnityEngine;
using System.Collections;

public class WX08_007 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (sc.isChanted())
        {
            sc.funcTargetIn(player, Fields.SIGNIZONE, check);
            sc.setEffect_sukinakazu(-1, player, Motions.GoHand);
        }
	}

    bool check(int x, int target)
    {
        return ms.getCardLevel(x, target) <= 3;
    }
}


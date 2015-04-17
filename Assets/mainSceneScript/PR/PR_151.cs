using UnityEngine;
using System.Collections;

public class PR_151 : MonoCard{

	// Use this for initialization
	void Start () {
        beforeStart();
        sc.attackArts = true;
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.changeColorCost(cardColorInfo.黒, 1);
        sc.Cost[0] = 6 - sc.getFieldTypeNum(player, Fields.LRIGTRASH, cardTypeInfo.アーツ) * 2;

        if (sc.isChanted())
        {
            sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.EnaCharge);
        }
	}

}

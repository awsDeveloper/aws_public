using UnityEngine;
using System.Collections;

public class WX09_041 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void chant()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, new checkFuncs(ms, cardColorInfo.白).check);

        if( sc.getFuncNum(new checkFuncs(ms, cardColorInfo.白).check, player) == 3 && sc.isShareClassExist(player))
            sc.setEffect(-2, 0, Motions.GoHand);
    }
}


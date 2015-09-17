using UnityEngine;
using System.Collections;

public class WX08_042 : MonoCard {

	// Use this for initialization
	void Start () {
        var al = sc.AddEffectTemplete(trigger);
        al.addEffect(alwys);


        sc.WhiteEnaFlag = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool trigger()
    {
        int eID = ms.getExitID(Fields.ENAZONE, Fields.SIGNIZONE);
        if (eID < 0)
            return false;

        return eID == ID + 50 * player || (sc.isOnBattleField() && eID / 50 == player);
    }

    void alwys()
    {
        sc.setEffect(0, player, Motions.TopEnaCharge);
    }
}


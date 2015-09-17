using UnityEngine;
using System.Collections;

public class WX08_039 : MonoCard {

	// Use this for initialization
	void Start () {
        var al = gameObject.AddComponent<EffectTemplete>();
        al.setTrigger(trigger, false, true);
        al.addEffect(alwys);

        var ig = sc.AddEffectTemplete(EffectTemplete.triggerType.Ignition, igni_1,true);
        ig.addEffect(igni_2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool trigger()
    {
        if (!sc.isOnBattleField())
            return false;

        int fID = ms.getOneFrameID(OneFrameIDType.FreezedID);
        return fID >= 0 && fID / 50 == 1 - player ;
    }

    void alwys()
    {
        sc.setEffect(0, 1 - player, Motions.oneHandDeath);
    }


    void igni_1()
    {
        //怪しい
        if (ms.getFieldAllNum((int)Fields.HAND, 1 - player) != 0)
            return;

        sc.setPayCost(cardColorInfo.青, 1);
        sc.setDown();
    }

    void igni_2()
    {
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.EnaCharge);
    }
}


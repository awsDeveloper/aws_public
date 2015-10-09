using UnityEngine;
using System.Collections;

public class WX09_021 : MonoCard {

	// Use this for initialization
	void Start () {

        sc.AddEffectTemplete(EffectTemplete.triggerType.Ignition, igni, true).addEffect(igni_1, false, EffectTemplete.checkType.True);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst).addEffect(burst_1);
	}
	
	// Update is called once per frame
	void Update () {
        ms.upAlwaysFlag(alwysEffs.FiaVix, 1 - player, ID, player);
	
	}

    void igni()
    {
        sc.setFuncEffect(-1, Motions.CostGoTrash, player, Fields.SIGNIZONE, new checkFuncs(ms, cardClassInfo.精武_毒牙).check);
        sc.costGoTrashIDenable = true;
    }

    void igni_1()
    {
        int x = ms.getCostGoTrashID(ID,player);
        sc.powerUpValue = -2000 * ms.getCardLevel(x);

        sc.setFuncEffect(-1, Motions.PowerUpEndPhase, 1 - player, Fields.SIGNIZONE, null);
    }

    void burst()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.TRASH, new checkFuncs(ms, cardClassInfo.精武_毒牙).check);
    }

    void burst_1()
    {
        sc.setFuncEffect(-1, Motions.PowerUpEndPhase, 1 - player, Fields.SIGNIZONE, null);
        sc.powerUpValue = -8000;
    }
}


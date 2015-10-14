using UnityEngine;
using System.Collections;

public class PR_173 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip);
        sc.AddEffectTemplete(tri).addEffect(triggered);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void cip()
    {
        sc.setFuncEffect(-1, Motions.EnaCharge, 1 - player, Fields.SIGNIZONE, ms.checkFreeze);
    }

    bool tri()
    {
        return sc.isOnBattleField() && ms.getStartedPhase() == (int)Phases.EndPhase;
    }

    void triggered()
    {
        sc.setFuncEffect(-1, Motions.FREEZE, 1 - player, Fields.SIGNIZONE, null);
    }

    void burst()
    {
        sc.setEffect(0, player, Motions.Draw);
    }
}


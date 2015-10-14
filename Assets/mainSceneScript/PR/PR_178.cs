using UnityEngine;
using System.Collections;

public class PR_178 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(tri).addEffect(alw);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool tri()
    {
        return sc.isOnBattleField() && ms.getGoTrashCharmID() >= 0;
    }

    void alw()
    {
        sc.powerUpValue = -4000;
        sc.setFuncEffect(-1, Motions.PowerUpEndPhase, 1 - player, Fields.SIGNIZONE, null);
    }
}


using UnityEngine;
using System.Collections;

public class WX08_006 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.resonaSummon(sc.notResonaAndKyotyu, 2);

        //triggered
        if (sc.isOnBattleField() && sc.getCipYourSigniID() >= 0)
            sc.setEffect(sc.getCipYourSigniID(), 0, Motions.TopSetCharm);

        //alwys
        ms.upAlwaysFlag(alwysEffs.Arachne, 1 - player, ID, player);

        if (ms.getStartedPhase() == (int)Phases.AttackPhase && ms.getTurnPlayer() == 1 - player)
        {
            sc.funcTargetIn(1 - player, Fields.SIGNIZONE, ms.havingCharm);
            sc.setEffect(-1, 0, Motions.EnaCharge);
            sc.effectSelecter = 1 - player;
        }
 	}
}

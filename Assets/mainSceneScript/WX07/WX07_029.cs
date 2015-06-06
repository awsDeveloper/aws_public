using UnityEngine;
using System.Collections;

public class WX07_029 : MonoCard {
    oneceTurnList onece;

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<CrossBase>().upBase = 15000;

        onece=gameObject.AddComponent<oneceTurnList>();
        onece.addOnce();
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.Ignition)
        {
            sc.Ignition=false;

            if (onece.onceIsFalse(0))
            {
                onece.onceUp(0);

                sc.Ignition = true;
                if (sc.IgnitionPayCost(cardColorInfo.緑, 1))
                {
                    sc.targetablePayCostRemove = true;
                    sc.funcTargetIn(player, Fields.ENAZONE);
                    sc.setEffect(-1, 0, Motions.GoHand);
                }
            }
        }

        if (sc.isHeaven())
            sc.setFieldAllEffect(player, Fields.SIGNIZONE, Motions.DoublePowerEnd);

        if (sc.isBursted())
            sc.minPowerBanish(8000);
	
	}
}


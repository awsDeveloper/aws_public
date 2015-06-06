using UnityEngine;
using System.Collections;

public class WX07_072 : MonoCard {
    bool flag = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.funcTargetIn(player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
            sc.powerUpValue = 5000;
            sc.TargetIDEnable = true;
            flag = true;
        }

        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.TargetIDEnable = false;

            if (sc.TargetID.Count > 0)
            {
                int tID = sc.TargetID[0];
                sc.TargetID.Clear();

                sc.setEffect(tID, 0, Motions.EnLancerEnd);
            }
        }


        if (sc.isBursted())
        {
            sc.funcTargetIn(player, Fields.MAINDECK, ms.checkHavingCross);
            sc.setEffect(-2, 0, Motions.GoHand);
        }

	
	}
}


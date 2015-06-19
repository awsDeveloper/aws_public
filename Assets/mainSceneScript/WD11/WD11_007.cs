using UnityEngine;
using System.Collections;

public class WD11_007 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.resonaSummon(sc.notResonaAndKyotyu, 2,true);

        if (sc.isCiped())
        {
            int x_1 = ms.getCostGoTrashID(ID, player);
            int x_2 = ms.getCostGoTrashID(ID, player);

            int levSum = 0;

            if (x_1 >= 0)
                levSum += ms.getCardLevel(x_1);
            if (x_2 >= 0)
                levSum += ms.getCardLevel(x_2);

            if (levSum != 0)
            {
                sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
                sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
                sc.powerUpValue = -2000 * levSum;
            }
        }
	}
}


using UnityEngine;
using System.Collections;

public class WX07_026 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        sc.beforeChantClassDownDownCost(cardClassInfo.精武_ウェポン, cardColorInfo.赤, 2);

        if (sc.isChanted())
        {
            sc.setEffect(0, 1 - player, Motions.TopCrash);
            sc.setEffect(0, 1 - player, Motions.TopCrash);
        }

        if (sc.isBursted())
        {
            sc.setEffect(0, 1 - player, Motions.PowerSumBanish);
            sc.addParameta(parametaKey.powerSumBanishValue, 8000);
        }
	}
}


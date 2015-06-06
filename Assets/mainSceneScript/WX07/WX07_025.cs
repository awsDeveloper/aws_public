using UnityEngine;
using System.Collections;

public class WX07_025 : MonoCard {
    bool flag = false;

	// Use this for initialization
	void Start () {
        var com = gameObject.AddComponent<CrossBase>();
        com.upBase = 15000;	
	}
	
	// Update is called once per frame
	void Update () {
        sc.cipDialog(cardColorInfo.赤, 1);


        if (sc.isHeaven())
        {
            sc.setDialogNum(DialogNumType.YesNo, cardColorInfo.赤, 1);
            if (sc.DialogFlag)
                flag = true;
        }

        if (sc.isMessageYes())
        {
            if (!flag)
            {
                sc.setEffect(ID, player, Motions.PayCost);
                sc.maxPowerBanish(7000);
            }
            else
            {
                flag = false;
                sc.setEffect(ID, player, Motions.PayCost);
                sc.maxPowerBanish(15000);
            }
        }

        if (sc.isBursted())
            sc.maxPowerBanish(10000);
	}
}


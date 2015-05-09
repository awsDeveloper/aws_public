using UnityEngine;
using System.Collections;

public class WD09_013 : MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();
	
	}
	
	// Update is called once per frame
	void Update () {
        cip();

        receive();
	}

    void receive()
    {
        if (sc.effectTargetID.Count > 0)
            return;

        int r = sc.getMessageInt();
        if (r == -1)
            return;

        for (int i = 0; true; i++)
        {
            int x = ms.getShowZoneID(i,true);
            if (x < 0)
                break;

            if (ms.getCardLevel(x % 50, x / 50) == r + 1 && ms.checkType(x % 50, x / 50, cardTypeInfo.シグニ))
                sc.setEffect(x, 0, Motions.GoHand);
            else
                sc.setEffect(x, 0, Motions.GoDeck);
        }
    }

    void cip()
    {
        if (!sc.isCiped())
            return;

        sc.setDialogNum(DialogNumType.Level);

        sc.setEffect(0, player, Motions.TopGoShowZone);
    }
}


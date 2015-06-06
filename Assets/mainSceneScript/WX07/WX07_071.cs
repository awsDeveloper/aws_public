using UnityEngine;
using System.Collections;

public class WX07_071 : MonoCard {
    bool flag = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (sc.isChanted())
        {
            for (int i = 0; i < 5; i++)
                sc.setEffect(0, player, Motions.TopGoShowZone);
            flag = true;
        }

        if (flag &&  sc.effectTargetID.Count == 0)
        {
            flag = false;

            chant();
        }

	}

    void chant()
    {
        int index = 0;
        while (true)
        {
            int x = ms.getShowZoneID(index);

            if (x < 0)
                break;

            if (ms.checkColor(x % 50, x / 50, cardColorInfo.緑))
                sc.setEffect(x, 0, Motions.GoEnaZone);
            else
                sc.setEffect(x, 0, Motions.GoTrash);

            index++;
        }
    }
}


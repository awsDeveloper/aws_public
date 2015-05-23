using UnityEngine;
using System.Collections;

public class WX07_065 : MonoCard{

	// Use this for initialization
	void Start () {
        beforeStart();
	
	}
	
	// Update is called once per frame
	void Update () {

        sc.changeColorCost(cardColorInfo.青, 5 - sc.getFreezeNum(1 - player));

        chant();	
	}

    void chant()
    {
        if (!sc.isChanted())
            return;

        sc.addComEffctString = "WX07_065add";
        for (int i = 0; i < 50; i++)
        {
            if (ms.checkType(i, player, cardTypeInfo.シグニ))
                sc.setEffect(i, player, Motions.AddComponent);
        }
    }
}


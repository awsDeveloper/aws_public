using UnityEngine;
using System.Collections;

public class WX08_020 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            if (ms.checkLrigColor(player, cardColorInfo.黒))
            {
                for (int i = 0; i < 10; i++)
                    sc.setEffect(0, 1 - player, Motions.TopGoTrash);
            }
            else
            {
                for (int i = 0; i < 5; i++)
                    sc.setEffect(0, 1 - player, Motions.TopGoTrash);
            }
        }
	
	}
}


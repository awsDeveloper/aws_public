using UnityEngine;
using System.Collections;

public class WX08_014 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.useLimit = ms.getLrigLevel(1 - player) < 4;

        if (sc.isChanted())
            sc.setEffect(0, 1 - player, Motions.DontGrow);
	
	}
}


using UnityEngine;
using System.Collections;

public class WX07_041 : MonoCard {

	// Use this for initialization
	void Start () {

        gameObject.AddComponent<CrossBase>().upBase = 8000;
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isHeaven() || sc.isBursted())
            sc.setEffect(0, player, Motions.Draw);
	
	}
}


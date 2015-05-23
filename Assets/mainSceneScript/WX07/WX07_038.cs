using UnityEngine;
using System.Collections;

public class WX07_038 : MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();
        var com = gameObject.AddComponent<CrossBase>();
        com.upBase = 8000;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!sc.isHeaven() && !sc.isBursted())
            return;
        sc.setEffect(0, player, Motions.Draw);
 
	}
}


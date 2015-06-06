using UnityEngine;
using System.Collections;

public class WX07_046 : MonoCard {
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        if (sc.CrossingCip())
        {
            sc.setEffect(ID, player, Motions.TopSetCharm);

            sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
            sc.powerUpValue = -5000;
        }

    }
}


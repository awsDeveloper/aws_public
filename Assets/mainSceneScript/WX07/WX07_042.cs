using UnityEngine;
using System.Collections;

public class WX07_042 : MonoCard {
    oneceTurnList once;
	// Use this for initialization
	void Start () {
        once = gameObject.AddComponent<oneceTurnList>();

        once.addOnce();
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isOnBattleField() && ms.LancerCrashedID != -1 && ms.CrasherID / 50 == player)
            sc.setEffect(0, player, Motions.Draw);


        if (sc.Ignition)
        {
            sc.Ignition = false;

            if(once.onceIsFalse(0)){
                once.onceUp(0);

                sc.powerUpValue = -5000;

                sc.setEffect(ID, player, Motions.CostPowerUpEnd);
                sc.setEffect(ID, player, Motions.EnLancerEnd);
            }
        }
	}
}


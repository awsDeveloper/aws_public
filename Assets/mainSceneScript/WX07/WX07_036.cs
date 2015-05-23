using UnityEngine;
using System.Collections;

public class WX07_036 : MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();
	
	}
	
	// Update is called once per frame
	void Update () {
        always();
	}

    void always()
    {
        if (!sc.isOnBattleField())
            return;

        int bID = ms.getBanishedID();
        int eID = ms.getEffecterNowID();

        if (bID < 0 || eID < 0)
            return;

        if (eID / 50 != player
            || ms.getFieldInt(eID % 50, eID / 50) != (int)Fields.SIGNIZONE
            || !ms.checkClass(eID, cardClassInfo.精武_ウェポン)
            || bID/50 != 1 - player)
            return;

        sc.setEffect(ID, player, Motions.EnDoubleCrashEnd);
    }
}


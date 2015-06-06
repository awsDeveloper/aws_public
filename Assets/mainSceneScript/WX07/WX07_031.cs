using UnityEngine;
using System.Collections;

public class WX07_031 : MonoCard {
    bool flag = false;

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<CrossBase>().upBase = 15000;

        var com = gameObject.AddComponent<FuncPowerUp>();
        com.setTrueTrigger(2000, ms.havingCharm);
	}
	
	// Update is called once per frame
	void Update () {

        if (sc.isHeaven() && ms.getCharmNum(player) > 0)
            sc.setDialogNum(DialogNumType.YesNo);
        

        if (sc.isMessageYes())
        {
            ms.targetableCharmIn(player, sc);

            sc.setEffect(-1, 0, Motions.GoTrash);

            if (sc.effectFlag)
                flag = true;
        }


        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.Targetable.Clear();

            sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
            sc.powerUpValue = -8000;
        }

        if (sc.isBursted())
        {
            sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.PowerUpEndPhase);

            sc.powerUpValue = -10000;
            if (ms.getCharmNum(player) > 0)
                sc.powerUpValue = -20000;

        }
	}    
}


using UnityEngine;
using System.Collections;

public class WX07_045 : MonoCard {
    bool flag = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isCiped())
        {
            sc.cursorCancel = true;
            ms.targetableCharmIn(player, sc);
            for (int i = 0; i < sc.Targetable.Count; i++)
                sc.setEffect(-1, 0, Motions.GoTrash);

            flag = true;
        }

        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.Targetable.Clear();
            sc.cursorCancel = false;
            int r = sc.inputReturn;

            if (r > 0)
            {
                sc.powerUpValue = -7000 * r;
                sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
                sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
            }
        }

        if (sc.IgnitionDownPayCost(cardColorInfo.黒, 1))
        {
            sc.setEffect(ID, player, Motions.trashCardAkumaCharm);
            sc.setEffect(ID, player, Motions.trashCardAkumaCharm);
            sc.setEffect(ID, player, Motions.trashCardAkumaCharm);
        }	
	}
}


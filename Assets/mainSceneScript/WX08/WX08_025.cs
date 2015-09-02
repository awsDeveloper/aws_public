using UnityEngine;
using System.Collections;

public class WX08_025 : MonoCard {
    bool flag = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.useLimit = !sc.isCrossOnBattleField(player);

        if (sc.isOnBattleField() && ms.checkIsCrossExited())
            sc.setEffect(ID, player, Motions.EnaCharge);

        if (sc.myColorSigniHeaven(cardColorInfo.赤))
            sc.setEffect(0, 1-player, Motions.Damaging);

        if (sc.isBursted())
        {
            sc.setEffect(0, player, Motions.Draw);
            sc.setEffect(0, player, Motions.TopGoShowZone);
            flag = true;
        }

        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.showZoneTargIn(ms.checkHavingCross);
            for (int i = 0; i < sc.Targetable.Count; i++)
                sc.setEffect(sc.Targetable[i], 0, Motions.GoHand);
            sc.Targetable.Clear();
            sc.setEffect(-1, 0, Motions.ShowZoneGoTop);
        }
	}
}


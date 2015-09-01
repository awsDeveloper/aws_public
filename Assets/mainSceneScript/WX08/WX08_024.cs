using UnityEngine;
using System.Collections;

public class WX08_024 : MonoCard {

	// Use this for initialization
	void Start () {
        var com = gameObject.AddComponent<CrossBase>();
        com.upBase = 15000;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.Ignition)
        {
            sc.Ignition = false;

            sc.funcTargetIn(player, Fields.HAND, check);
            if (sc.Targetable.Count >= 3)
            {
                sc.setEffect(-1, 0, Motions.CostHandDeath);
                sc.setEffect(-1, 0, Motions.CostHandDeath);
                sc.setEffect(-1, 0, Motions.CostHandDeath);
                sc.setEffect(0, player, Motions.TopCrash);
                sc.setEffect(0, 1-player, Motions.TopCrash);
            }
            else
                sc.Targetable.Clear();
        }

        if (sc.isHeaven())
        {
            sc.setEffect(ID, player, Motions.EnAbility);
            sc.addParameta(parametaKey.EnAbilityType, (int)ability.assassin);
        }

        if (sc.isBursted())
            sc.minPowerBanish(sc.getMinPower(1 - player));
	}

    bool check(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精羅_鉱石) || ms.checkClass(x, target, cardClassInfo.精羅_宝石);
    }
}


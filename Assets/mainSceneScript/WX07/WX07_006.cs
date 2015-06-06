using UnityEngine;
using System.Collections;

public class WX07_006 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        resona();

        if (sc.isOnBattleField())
            ms.setSigniSummonLim(2, 1 - player, ID, player);
        else
            ms.normalizeSigniSummonLim(1 - player, ID, player);

	}

    void resona()
    {
        if (!sc.useResona)
            return;

        sc.useResona = false;

        ms.setSystemCostGoTrashLevelSumResona(7, sc.notResonaAndUtyu, ID, player);
    }
}


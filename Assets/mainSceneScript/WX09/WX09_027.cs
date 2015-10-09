using UnityEngine;
using System.Collections;

public class WX09_027 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(cipTri).addEffect(cip);
	}
	
	// Update is called once per frame
	void Update () {
        ms.upAlwaysFlag(alwysEffs.Orichaltia, player, ID, player);
	
	}

    bool cipTri()
    {
        return sc.isCiped() && sc.FieldContainsName(player, "アダマスフィア", Fields.TRASH);
    }

    void cip()
    {
        sc.maxPowerBanish(7000);
    }
}


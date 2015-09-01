using UnityEngine;
using System.Collections;

public class WX08_017 : MonoCard {
    bool flag = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.powerUpValue = 5000;
            sc.setEffect(0, player, Motions.PowerUpAllEnd);
        }

        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;

            sc.addParameta(parametaKey.EnAbilityType, (int)ability.resiArts);
            sc.minPowerTargetIn(30000, true);
            for (int i = 0; i < sc.Targetable.Count; i++)
                sc.setEffect(sc.Targetable[i], 0, Motions.EnAbility);
            sc.Targetable.Clear();
        }
	}
}


using UnityEngine;
using System.Collections;

public class WX07_077 : MonoCard {
    bool flag = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.funcTargetIn(player, Fields.HAND);
            sc.setEffect(-1, 0, Motions.HandDeath);
            if (sc.effectFlag)
                flag = true;
        }

        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.Targetable.Clear();


            sc.minPowerBanish(0);
        }
	}
}


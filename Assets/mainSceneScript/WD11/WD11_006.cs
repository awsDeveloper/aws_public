using UnityEngine;
using System.Collections;

public class WD11_006 : MonoCard {
    bool flag = false;
	// Use this for initialization
	void Start () {
        sc.attackArts = true;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.funcTargetIn(player, Fields.HAND, check);
            sc.setEffect_sukinakazu(-1, 0, Motions.HandDeath);
            flag = true;
        }

        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.cursorCancel = false;
            sc.Targetable.Clear();


            sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
            sc.powerUpValue = -2000*sc.getExecutedLevelSum(Motions.HandDeath);
        }
	
	}

    bool check(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精生_凶蟲);
    }
}


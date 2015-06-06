using UnityEngine;
using System.Collections;

public class WX07_021 : MonoCard
{
    bool flag = false;

    // Use this for initialization
    void Start()
    {
        sc.attackArts = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (sc.isChanted())
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

            sc.powerUpValue = -8000 * sc.inputReturn;
            sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
        }

    }
}


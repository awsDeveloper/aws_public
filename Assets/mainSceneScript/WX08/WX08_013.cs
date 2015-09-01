using UnityEngine;
using System.Collections;

public class WX08_013 : MonoCard {

    // Use this for initialization
    void Start()
    {
        sc.attackArts = true;
        sc.notMainArts = true;
    }

    // Update is called once per frame
    void Update()
    {
        sc.useLimit = !sc.isCrossOnBattleField(player);

        if (sc.isChanted())
            sc.setFieldAllEffect(1 - player, Fields.SIGNIZONE, Motions.DownAndFreeze);
    }
}


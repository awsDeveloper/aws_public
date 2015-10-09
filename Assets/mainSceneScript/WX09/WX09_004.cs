using UnityEngine;
using System.Collections;

public class WX09_004 : MonoCard {

    // Use this for initialization
    void Start()
    {
        sc.attackArts = true;

        gameObject.AddComponent<FuncChangeCost>().set(costCheck, cardColorInfo.青, 3);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);
    }

    // Update is called once per frame
    void Update()
    {


    }

    bool costCheck()
    {
        return ms.getTurnPlayer() == 1 - player;
    }

    void chant()
    {
        sc.setEffect(0, player, Motions.Draw);
        sc.setEffect(0, player, Motions.Draw);
        sc.setEffect(0, player, Motions.Draw);
    }
}


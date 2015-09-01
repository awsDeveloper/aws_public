using UnityEngine;
using System.Collections;

public class WX08_018 : MonoCard {

    // Use this for initialization
    void Start()
    {
        sc.attackArts = true;
        sc.SpellCutIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        sc.useLimit = !sc.isCrossOnBattleField(player);

        if (sc.isChanted())
        {
            sc.addParameta(parametaKey.EnAbilityType, (int)ability.resiBanish);
            sc.ClassTargetIn(player, Fields.SIGNIZONE, cardClassInfo.精像_美巧);
            for (int i = 0; i < sc.Targetable.Count; i++)
                sc.setEffect(sc.Targetable[i], 0, Motions.EnAbility);
            sc.Targetable.Clear();
        }
    }
}


using UnityEngine;
using System.Collections;

public class WX09_053 : MonoCard {

    // Use this for initialization
    void Start()
    {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void chant()
    {
        sc.powerUpValue = 5000;
        sc.setFieldAllEffect(player, Fields.SIGNIZONE, Motions.PowerUpEndPhase);

        if (sc.getFuncNum(new checkFuncs(ms, cardColorInfo.緑).check, player) == 3 && sc.isShareClassExist(player))
        {
            sc.setFieldAllEffect(player, Fields.SIGNIZONE, Motions.EnAbility);
            sc.addParameta(parametaKey.EnAbilityType, (int)ability.TopChargeAfterAttack);
        }
    }
}


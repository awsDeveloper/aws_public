using UnityEngine;
using System.Collections;

public class WX09_019 : MonoCard {
    FuncPowerUp fpu;
	// Use this for initialization
	void Start () {
        fpu = gameObject.AddComponent<FuncPowerUp>();
        fpu.setForMe(0, null);

        sc.AddEffectTemplete(tri).addEffect(alw_jibaku);

        gameObject.AddComponent<funcSetAbility>().set(alwChe_1, ability.resiArts);

        gameObject.AddComponent<funcSetAbility>().set(alwChe_2, ability.Lancer);
        gameObject.AddComponent<funcSetAbility>().set(alwChe_2, ability.TwoBanishAfterCrash);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
	}
	
	// Update is called once per frame
	void Update () {
        fpu.setPUV(ms.getFieldAllNum((int)Fields.ENAZONE, player) * 2000);

	}

    bool tri()
    {
        return !sc.effectFlag && sc.isOnBattleField() && ms.getCardPower(ID, player) >= 20000;
    }

    void alw_jibaku() {
        sc.setEffect(ID, player, Motions.GoTrash);
    }

    bool alwChe_1()
    {
        return sc.isOnBattleField() && ms.getCardPower(ID, player) >= 14000;
    }

    bool alwChe_2()
    {
        return sc.isOnBattleField() && ms.getCardPower(ID, player) >= 18000;
    }

    void burst()
    {
        sc.setEffect(0, player, Motions.TopEnaCharge);
    }
}


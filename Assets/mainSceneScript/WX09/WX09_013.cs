using UnityEngine;
using System.Collections;

public class WX09_013 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<NameCostResona>().set("幻獣　ハチ", "幻獣　モンキ", "幻獣　キジ");

        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip).addEffect(cip);

        gameObject.AddComponent<funcSetAbility>().set(checkPhase, ability.resiBanish);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Ignition, igni, true).addEffect(igni_1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void cip()
    {
        sc.setEffect(0, player, Motions.TopEnaCharge);
        sc.setEffect(0, player, Motions.TopEnaCharge);
        sc.setEffect(0, player, Motions.TopEnaCharge);
    }

    bool checkPhase()
    {
        return (Phases)ms.getPhaseInt() == Phases.AttackPhase;
    }

    void igni()
    {
        sc.setPayCost(cardColorInfo.緑, 1);
    }

    void igni_1()
    {
        sc.setEffect(ID, player, Motions.EnAbility);
        sc.addParameta(parametaKey.EnAbilityType, (int)ability.Lancer);
    }
}


using UnityEngine;
using System.Collections;

public class WX09_017 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<FuncChangeBase>().set(alwChe, 15000);
        gameObject.AddComponent<funcSetAbility>().set(alwChe, ability.DoubleCrash);
        gameObject.AddComponent<funcSetAbility>().set(alwChe_1, ability.resiNotArts);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool alwChe()
    {
        return ms.getClassNum(player, Fields.TRASH, cardClassInfo.精羅_鉱石) >= 5;
    }
    bool alwChe_1()
    {
        return ms.getClassNum(player, Fields.TRASH, cardClassInfo.精羅_宝石) >= 5;
    }

    void burst()
    {
        sc.maxPowerBanish(ms.getClassNum(player, Fields.TRASH, cardClassInfo.鉱石または宝石) * 3000);
    }
}


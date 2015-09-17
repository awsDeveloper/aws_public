using UnityEngine;
using System.Collections;

public class WX08_005 : MonoCard {

	// Use this for initialization
	void Start () {

        var re = sc.AddEffectTemplete(EffectTemplete.triggerType.useResona);
        re.addEffect(resona, true, EffectTemplete.checkType.system);
        re.addEffect(resona_2, true, EffectTemplete.checkType.system);
        re.addEffect(resona_3, false, EffectTemplete.checkType.system);
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isOnBattleField())
            ms.setColorChangeIDs(ID, player, cardColorInfo.白);

        sc.setAbility(ability.resiYourWhiteCardEff,true);	
	}

    void resona()
    {
        sc.funcTargetIn(player, Fields.SIGNIZONE, checkResona);
        ms.SetSystemCardFromCard(-1, Motions.GOLrigTrash, ID, player, sc.Targetable);
    }

    void resona_2() {
        sc.funcTargetIn(player, Fields.SIGNIZONE, checkLevel);
        ms.SetSystemCardFromCard(-1, Motions.CostGoTrash, ID, player, sc.Targetable);
    }

    void resona_3()
    {
        ms.SetSystemCardFromCard(ID + 50 * player, Motions.Summon, ID, player);
    }

    bool checkResona(int x,int target)
    {
        return ms.checkType(x, target, cardTypeInfo.レゾナ);
    }

    bool checkLevel(int x, int target)
    {
        return ms.getCardLevel(x, target) <= 3 && !ms.checkType(x, target, cardTypeInfo.レゾナ);
    }
}


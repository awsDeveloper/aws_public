using UnityEngine;
using System.Collections;

public class WX08_023 : MonoCard {

	// Use this for initialization
	void Start () {
        var t = gameObject.AddComponent<SearchOrSummonBurst>();
        t.setMyClass(cardClassInfo.精羅_宇宙);
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.banishedDialog(cardColorInfo.無色, 0);
        if (sc.isMessageYes(dialogUpConditions.banished))
        {
            sc.funcTargetIn(player, Fields.HAND, checkUtyu);
            if (sc.Targetable.Count >= 2)
            {
                sc.setEffect(-1, 0, Motions.HandDeath);
                sc.setEffect(-1, 0, Motions.HandDeath);
                sc.setEffect(ID, player, Motions.Summon);
            }
            else
                sc.Targetable.Clear();
        }


        //cip
        sc.cipDialog(cardColorInfo.白, 1);
        if (sc.isMessageYes(dialogUpConditions.cip))
        {
            sc.setPayCost();
            sc.funcTargetIn(1 - player, Fields.TRASH);
            sc.setEffect(-2, 0, Motions.Exclude);
        }

        if (sc.IgnitionDownPayCost(cardColorInfo.白, 1))
            sc.setFuncEffect(-2, Motions.GoLrigDeck, player, Fields.LRIGTRASH, checkResona);


	}

    bool checkResona(int x, int target)
    {
        return ms.checkType(x, target, cardTypeInfo.レゾナ);
    }

    bool checkUtyu(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精羅_宇宙);
    }
}


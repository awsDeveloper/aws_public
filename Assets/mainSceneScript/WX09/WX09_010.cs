using UnityEngine;
using System.Collections;

public class WX09_010 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<NameCostResona>().set("甲冑　ローメイル", "篭手　トレット");

        gameObject.AddComponent<FuncPowerUp>().set(1000, null, null, sc.player, true);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Banished, banished);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void cip()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, null);
    }

    void banished()
    {
        sc.setFuncEffect(-2, Motions.Summon, player, Fields.MAINDECK, checkName);
    }

    bool checkName(int x, int target)
    {
        return ms.checkName(x, target, "篭手　トレット");
    }
}


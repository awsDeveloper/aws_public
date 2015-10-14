using UnityEngine;
using System.Collections;

public class PR_212 : MonoCard {

	// Use this for initialization
    void Start()
    {
        var ig = sc.AddEffectTemplete(EffectTemplete.triggerType.useAttackArts);
        ig.addEffect(igni_cost, EffectTemplete.option.cost);
        ig.addEffect(igni, EffectTemplete.option.ifThen);
        ig.addEffect(igni_1);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst).addEffect(burst_1);
    }
	
	// Update is called once per frame
	void Update () {
        sc.attackArts = ms.getFieldInt(ID, player) == (int)Fields.TRASH;
	
	}

    void igni_cost()
    {
        var c=new colorCostArry( cardColorInfo.黒,2);
        c.addCost( cardColorInfo.無色,1);
        sc.changeCost(c);
        sc.setPayCost();
    }

    bool igniChe(int x, int target)
    {
        return !ms.checkColor(x, target, cardColorInfo.無色) && ms.checkType(x, target, cardTypeInfo.シグニ);
    }

    void igni()
    {
        sc.funcTargetIn(player, Fields.TRASH, igniChe);
        if (sc.getTargLevNum() < 4)
        {
            sc.Targetable.Clear();
            return;
        }

        sc.targetableSameLevelRemove = true;
        for (int i = 0; i < 4; i++)
            sc.setEffect(-2, 0, Motions.GoDeckBottom);
    }

    void igni_1()
    {
        sc.targetableSameLevelRemove = false;

        if (ms.getFieldInt(ID, player) == (int)Fields.TRASH)
            sc.setEffect(ID, player, Motions.Summon);

        sc.setEffect(0, player, Motions.Shuffle);
    }

    void burst()
    {
        sc.setEffect(0, player, Motions.TopGoTrash);
        sc.setEffect(0, player, Motions.TopGoTrash);
        sc.setEffect(0, player, Motions.TopGoTrash);
    }

    void burst_1()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.TRASH, igniChe);
    }
}


using UnityEngine;
using System.Collections;

public class WX08_036 : MonoCard {

	// Use this for initialization
	void Start () {
        var ig_1 = sc.AddEffectTemplete(EffectTemplete.triggerType.Ignition, igni_1_1, true);
        ig_1.addEffect(igni_1_2, EffectTemplete.option.ifThen);
        ig_1.addEffect(igni_1_3);

        var ig_2 = sc.AddEffectTemplete(EffectTemplete.triggerType.Ignition, igni_2_1, true);
        ig_2.addEffect(igni_2_2, true);
        ig_2.addEffect(igni_2_3);
        ig_2.addEffect(igni_2_4);

        var ma= gameObject.AddComponent<EffTemManager>();
        ma.setTrigger(EffectTemplete.triggerType.Ignition);
        ma.setTemplete("パワー", ig_1);
        ma.setTemplete("レベル", ig_2);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void igni_1_1()
    {
        sc.setEffect(ID, player, Motions.Down);
    }

    void igni_1_2()
    {
        sc.ClassTargetIn(player, Fields.TRASH, cardClassInfo.鉱石または宝石);
        if (sc.Targetable.Count < 5)
        {
            sc.Targetable.Clear();
            return;
        }

        for (int i = 0; i < 5; i++)
            sc.setEffect(-2, 0, Motions.GoDeckBottom);
    }

    void igni_1_3()
    {
        sc.maxPowerBanish(10000);
    }


    void igni_2_1()
    {
        sc.setPayCost(cardColorInfo.赤, 1);
    }

    void igni_2_2()
    {
        sc.setEffect(ID, player, Motions.CostGoTrash);
    }

    void igni_2_3()
    {
        sc.setEffect(0, player, Motions.TopGoTrash);
        sc.setEffect(0, player, Motions.TopGoTrash);
        sc.setEffect(0, player, Motions.TopGoTrash);
        sc.setEffect(0, player, Motions.TopGoTrash);
        sc.setEffect(0, player, Motions.TopGoTrash);
    }

    void igni_2_4()
    {
        int index = 0;
        int count = 0;

        while (true)
        {
            int x = ms.getTopGoTrashListID(index, ID, player);

            if (x == -1)
                break;

            if (ms.checkClass(x, cardClassInfo.鉱石または宝石))
                count++;

            index++;
        }

        sc.LevelMaxTargetIn(1 - player, Fields.SIGNIZONE, count);
        sc.setEffect(-1, 0, Motions.EnaCharge);
    }
}


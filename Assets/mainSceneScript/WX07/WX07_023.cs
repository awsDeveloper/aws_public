using UnityEngine;
using System.Collections;

public class WX07_023 : MonoCard {
    bool flag = false;

	// Use this for initialization
	void Start () {
        var com= gameObject.AddComponent<DialogToggle>();
        com.set("ダウン", effect_1);
        com.set("サーチ", effect_2);
        com.set("サモン", effect_3);

        com.setTrigger(DialogToggle.triggerType.Burst);	
	}
	
	// Update is called once per frame
	void Update () {
        if (check())
            sc.cipDialog(cardColorInfo.白, 2);

        if (sc.DialogNum == 0 && sc.messages.Count>0)
        {
            string r = sc.messages[0];
            sc.messages.RemoveAt(0);

            if (r == "Yes")
            {
                sc.setEffect(ID, player, Motions.PayCost);

                sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
                sc.setEffect(-1, 0, Motions.GoHand);

                sc.setCanUseFunc(check);
            }

            flag = true;
        }

        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.Targetable.Clear();

            sc.funcTargetIn(player, Fields.SIGNIZONE, checkResona);
            sc.setEffect(-1, 0, Motions.EnAbility);

            sc.addParameta(parametaKey.EnAbilityType, (int)ability.resiArts);
        }
	
	}

    bool effect_1()
    {
        sc.setEffect(ms.getLrigID(1 - player), 1 - player, Motions.EffectDown);
        return true;
    }

    bool effect_2()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, checkE_2);
        return true;
    }

    bool checkE_2(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精羅_宇宙);
    }

    bool effect_3()
    {
        sc.setFuncEffect(-2, Motions.Summon, player, Fields.MAINDECK, checkE_2);
        return true;
    }

    bool check()
    {
        return sc.isResonaOnBattleField(player);
    }

    bool checkResona(int x,int target)
    {
        return ms.checkType(x, target, cardTypeInfo.レゾナ);
    }
}


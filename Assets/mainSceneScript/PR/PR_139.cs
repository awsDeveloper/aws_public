using UnityEngine;
using System.Collections;

public class PR_139 : MonoCard {
    bool afterEffect_1 = false;
    bool afterEffect_2 = false;

	// Use this for initialization
	void Start () {
        beforeStart();

        DialogToggle toggle = gameObject.AddComponent<DialogToggle>();
        toggle.setTrigger(DialogToggle.triggerType.Chant);
        toggle.set("バニッシュ", check_1, effect_1);
        toggle.set("エナ破壊", check_2, effect_2);
	}
	
	// Update is called once per frame
	void Update () {
         if (sc.TargetID.Count > 0)
        {
            sc.TargetIDEnable = false;
            sc.TargetID.Clear();

            sc.Targetable.Clear();

            if (afterEffect_1)
            {
                afterEffect_1 = false;
                sc.funcTargetIn(1 - player, Fields.SIGNIZONE, hantei_1);
                sc.setEffect(-1, 0, Motions.EnaCharge);
            }

            if (afterEffect_2)
            {
                afterEffect_2 = false;
                sc.funcTargetIn(1 - player, Fields.ENAZONE, hantei_2);
                sc.setEffect(-1, 0, Motions.GoTrash);
                sc.setEffect(0, 1-player, Motions.EnaSort);
            }
        }
	
	}

    bool hantei_1(int x, int target)
    {
        return ms.getCardPower(x, target) <= 8000;
    }

    bool hantei_2(int x, int target)
    {
        return ms.getCardScr(x, target).MultiEnaFlag;
    }

    bool check_1()
    {
        return sc.getMinPower(1-player) <= 8000;
    }

    bool check_2()
    {
        return ms.MultiEnaNum(1 - player) > 0;
    }

    bool effect_1()
    {
        afterEffect_1 = true;

        sc.TargetIDEnable = true;
        sc.BeforeCutInNum = 2;
        sc.setEffect(-1, 0, Motions.CostBanish);
        return true;
    }

    bool effect_2()
    {
        afterEffect_2 = true;

        sc.TargetIDEnable = true;
        sc.BeforeCutInNum = 2;
        sc.setEffect(-1, 0, Motions.CostBanish);
        return true;
    }
}

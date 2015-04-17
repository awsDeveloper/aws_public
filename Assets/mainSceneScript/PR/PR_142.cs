using UnityEngine;
using System.Collections;

public class PR_142 : MonoCard {
    bool afterEffect_1 = false;
    bool afterEffect_2 = false;

    // Use this for initialization
    void Start()
    {
        beforeStart();

        DialogToggle toggle = gameObject.AddComponent<DialogToggle>();
        toggle.setTrigger(DialogToggle.triggerType.Chant);
        toggle.set("サーチ", check_1, effect_1);
        toggle.set("パワーダウン", check_2, effect_2);
    }

    // Update is called once per frame
    void Update()
    {
        if (sc.TargetID.Count > 0)
        {
            sc.TargetIDEnable = false;
            sc.TargetID.Clear();

            sc.Targetable.Clear();

            if (afterEffect_1)
            {
                afterEffect_1 = false;
                sc.funcTargetIn(player, Fields.MAINDECK, hantei_1);
                sc.setEffect(-2, 0, Motions.GoHand);
            }

            if (afterEffect_2)
            {
                afterEffect_2 = false;
                sc.funcTargetIn(1 - player, Fields.SIGNIZONE, hantei_2);

                sc.powerUpValue = -7000;
                sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
            }
        }

    }

    //バニッシュ後のサーチの判定およびバニッシュの対象のチェック
    bool hantei_1(int x, int target)
    {
        return ms.checkColor(x,target, cardColorInfo.白);
    }

    //バニッシュ後のパワーダウンの判定
    bool hantei_2(int x, int target)
    {
        return true;
    }

    //バニッシュの対象のチェック
    bool hantei_3(int x, int target)
    {
        return ms.checkColor(x, target, cardColorInfo.黒);
    }

    bool check_1()
    {
        sc.funcTargetIn(player, Fields.SIGNIZONE, hantei_1);
        if (sc.Targetable.Count == 0)
            return false;
        sc.Targetable.Clear();

        return true;
    }

    bool check_2()
    {
        sc.funcTargetIn(player, Fields.SIGNIZONE, hantei_3);
        if (sc.Targetable.Count == 0)
            return false;

        sc.Targetable.Clear();

        return sc.isSigniOnBattleField(1 - player);
    }

    bool effect_1()
    {
        sc.funcTargetIn(player, Fields.SIGNIZONE, hantei_1);
        if(sc.Targetable.Count==0)
            return false;

        sc.setEffect(-1, 0, Motions.EnaCharge);

        afterEffect_1 = true;

        sc.TargetIDEnable = true;
        sc.BeforeCutInNum = 1;

        return true;
    }

    bool effect_2()
    {
        sc.funcTargetIn(player, Fields.SIGNIZONE, hantei_3);
        if (sc.Targetable.Count == 0)
            return false;

        sc.setEffect(-1, 0, Motions.EnaCharge);

        afterEffect_2 = true;

        sc.TargetIDEnable = true;
        sc.BeforeCutInNum = 2;

        return true;
    }
}

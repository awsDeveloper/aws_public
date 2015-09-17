using UnityEngine;
using System.Collections;

public class WX08_051 : MonoCard {

    // Use this for initialization
    void Start()
    {
        gameObject.AddComponent<FuncChangeBase>().set(check_alw, 5000);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip, true, true).addEffect(cip_2);

    }

    // Update is called once per frame
    void Update()
    {

    }

    bool check_alw()
    {
        return sc.isFuncOnBatteField(check_korokaro, player) || sc.isFuncOnBatteField(check_diamu, player);
    }

    void cip()
    {
        sc.setPayCost(cardColorInfo.白, 1);
    }

    void cip_2()
    {
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, check_korokaro);
    }


    bool check_korokaro(int x, int target)
    {
        return ms.checkName(x, target, "羅星　コロカロ");
    }

    bool check_diamu(int x, int target)
    {
        return ms.checkName(x, target, "羅星　ディアデム");
    }
}


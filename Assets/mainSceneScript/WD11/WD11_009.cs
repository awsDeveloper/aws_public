using UnityEngine;
using System.Collections;

public class WD11_009 : MonoCard
{
    // Use this for initialization
    void Start()
    {
        var com = gameObject.AddComponent<FuncChangeBase>();
        com.setFunc(sc.isResonaOnBattleField);
        com.baseValue = 15000;
    }

    // Update is called once per frame
    void Update()
    {
        sc.cipDialog(cardColorInfo.黒, 1);

        if (sc.isMessageYes())
            sc.setPayCost();            

        if (sc.PayedCostFlag)
        {
            sc.PayedCostFlag = false;
            sc.funcTargetIn(player, Fields.TRASH, check);
            sc.setEffect(-2, 0, Motions.Summon);
        }


        if (sc.isBursted())
        {
            sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
            sc.powerUpValue = -10000;
        }
    }

    bool check(int x,int target)
    {
        return ms.getCardLevel(x, target) <= 2 && ms.checkColorType(x, target, cardColorInfo.黒, cardTypeInfo.シグニ);
    }
}


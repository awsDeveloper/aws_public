using UnityEngine;
using System.Collections;

public class WD11_008 : MonoCard {

	// Use this for initialization
	void Start () {

        var com= gameObject.AddComponent<DialogToggle>();
        com.setTrigger(DialogToggle.triggerType.useResona,2,true);

        com.setAction("手札", effect_1, check_1);
        com.setAction("エナゾーン", effect_2, check_2);
        com.setAction("場", effect_3, check_3);

        com.setFienalAction(DialogToggle.FinalActionType.summonThisResona);
    }
	
	// Update is called once per frame
	void Update () {
        if (sc.isCiped())
        {
            sc.LevelMaxTargetIn(1-player, Fields.SIGNIZONE, 2);
            sc.setEffect(-1, 0, Motions.EnaCharge);
        }
	
	}


    bool checkClass(int x,int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精生_凶蟲);
    }

    void effect_1()
    {
        sc.funcTargetIn(player, Fields.HAND, checkClass);
        ms.SetSystemCardFromCard(-1, Motions.CostHandDeath, ID, player,sc.Targetable);
    }
    bool check_1()
    {
        sc.funcTargetIn(player, Fields.HAND, checkClass);
        return sc.checkExistTargetable();
    }


    void effect_2()
    {
        sc.funcTargetIn(player, Fields.ENAZONE, checkClass);
        ms.SetSystemCardFromCard(-1, Motions.CostGoTrash, ID, player, sc.Targetable);
    }
    bool check_2()
    {
        sc.funcTargetIn(player, Fields.ENAZONE, checkClass);
        return sc.checkExistTargetable();
    }


    void effect_3()
    {
        sc.funcTargetIn(player, Fields.SIGNIZONE, sc.notResonaAndKyotyu);
        ms.SetSystemCardFromCard(-1, Motions.CostGoTrash, ID, player, sc.Targetable);
    }
    bool check_3()
    {
        sc.funcTargetIn(player, Fields.SIGNIZONE, sc.notResonaAndKyotyu);
        return sc.checkExistTargetable();
    }
}


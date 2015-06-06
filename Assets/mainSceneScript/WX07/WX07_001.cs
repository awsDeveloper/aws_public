using UnityEngine;
using System.Collections;

public class WX07_001 : MonoCard {
    bool igniFlag = false;

	// Use this for initialization
	void Start () {
 	
	}
	
	// Update is called once per frame
	void Update () {
        ignition();
        afterIgni();

        trigger();
        receive();
	
	}

    void ignition()
    {
        if (!sc.IgnitionPayCost(cardColorInfo.白, 1))
            return;

        sc.setFuncEffect(-1, Motions.GOLrigTrash ,player, Fields.SIGNIZONE, checkIgni);
        igniFlag = true;
    }

    void afterIgni()
    {
        if (!igniFlag || sc.effectTargetID.Count > 0)
            return;

        igniFlag = false;
        sc.Targetable.Clear();

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.GoTrash);
    }

    bool checkIgni(int x, int target)
    {
        return ms.checkType(x, target, cardTypeInfo.レゾナ);
    }

    void receive()
    {
        if (!sc.isMessageYes())
            return;

        sc.setEffect(ID, player, Motions.PayCost);

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.GoHand);
    }


    void trigger()
    {
        int sID=ms.getCipSigniID();
        if (!sc.isOnBattleField() 
            || sID < 0 
            || !ms.checkType(sID, cardTypeInfo.レゾナ)
            || sID/50!=player
            || ms.getTurnPlayer() != player)
            return;

        sc.changeColorCost(cardColorInfo.白, 1);
        if (!sc.myCheckCost())
            return;

        sc.setDialogNum(DialogNumType.YesNo);
    }
}


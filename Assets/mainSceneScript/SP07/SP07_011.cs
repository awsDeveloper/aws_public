using UnityEngine;
using System.Collections;

public class SP07_011 : MonoCard{

    bool upFlag = false;
    int levelLine = -1;

	// Use this for initialization
	void Start ()
	{
		beforeStart();

	}

	// Update is called once per frame
	void Update ()
	{
        always();

        burst();

        ignition();

        cip();

        receive();

        targetIDCheck();
	}

    void targetIDCheck()
    {
        if (sc.effectTargetID.Count > 0)
            return;

        int tID=sc.getTargetID();
        if (tID == -1)
            return;

        sc.TargetIDEnable = false;
        sc.Targetable.Clear();

        levelLine = ms.getCardLevel(tID % 50, tID / 50);
        sc.funcTargetIn(player, Fields.MAINDECK, hantei_2);
        sc.setEffect(-2, 0, Motions.Summon);
        sc.setEffect(0, player, Motions.Shuffle);
    }

    bool hantei_2(int x, int target)
    {
        return ms.getCardLevel(x, target) == levelLine + 1;
    }

    void receive()
    {
        if (!sc.isMessageYes())
            return;

        sc.setEffect(ID, player, Motions.PayCost);

        sc.funcTargetIn(player, Fields.SIGNIZONE, hantei);
        sc.setEffect(-1, 0, Motions.EnaCharge);
        sc.TargetIDEnable = true;
    }

    void cip()
    {
        if (!sc.isCiped())
            return;

        sc.changeColorCost(cardColorInfo.緑, 1);

        if (!sc.myCheckCost())
            return;

        sc.funcTargetIn(player, Fields.SIGNIZONE, hantei);
        if (sc.Targetable.Count == 0)
            return;

        sc.Targetable.Clear();
        sc.DialogFlag = true;

     }

    bool hantei(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精像_美巧);
    }

    void ignition()
    {
        if (!sc.Ignition)
            return;

        sc.Ignition = false;

        sc.changeColorCost(cardColorInfo.緑, 1);
        sc.Cost[1] = 1;

        if (!sc.myCheckCost())
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE, check);

        if (sc.Targetable.Count == 0)
            return;

        sc.setEffect(ID, player, Motions.PayCost);
        sc.setEffect(-1, 0, Motions.GoHand);
    }

    bool check(int x,int target)
    {
        return ms.getCardPower(x, target) >= 15000;
    }

    void always()
    {
        if (sc.isOnBattleField())
        {
            upFlag = true;
            ms.melhenFlag[player] = true;
        }
        else if (upFlag)
        {
            upFlag = false;
            ms.melhenFlag[player] = false;
        }
    }

    void burst()
    {
        if (!sc.isBursted())
            return;

        sc.setEffect(0, player, Motions.TopEnaCharge);
        sc.setEffect(0, player, Motions.TopEnaCharge);
    }
}

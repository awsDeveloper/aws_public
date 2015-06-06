using UnityEngine;
using System.Collections;

public class WX07_033 : MonoCard {
    bool rCip = false;
    bool bFlag = false;
    int bID = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.setAbilitySelf(ability.DontSelfGoTrash, true);

        resonaCip();
        banished();
        receive();

	}

    void banished()
    {
        if (!sc.isOnBattleField())
            return;

        if (bID != -1)
            return;

        bID = ms.getBanishedID();

        if (bID == -1 || ms.checkName(bID, "羅星　アルファード"))
        {
            bID = -1;
            return;
        }

        sc.setDialogNum(DialogNumType.YesNo);
        bFlag = true;
    }

    void resonaCip()
    {
        if (!sc.isTrashOrEna())
            return;

        int exID = ms.getExitID(-1, (int)Fields.SIGNIZONE);

        if (!ms.checkType(exID, cardTypeInfo.レゾナ)
            || exID / 50 != player)
            return;

        sc.changeColorCost(cardColorInfo.白, 1);
        if (!sc.myCheckCost())
            return;

        sc.setDialogNum(DialogNumType.YesNo);
        rCip = true;
    }

    void receive()
    {
        if (!sc.isMessageYes())
            return;

        if (rCip)
        {
            rCip = false;
            sc.setEffect(ID, player, Motions.PayCost);
            sc.setEffect(ID, player, Motions.DownSummonFromTrash);
        }

        if (bFlag)
        {
            bFlag = false;
            sc.setEffect(ID, player, Motions.EnaCharge);
            sc.setEffect(bID%50, bID/50, Motions.Summon);
            sc.setEffect(0, bID / 50, Motions.EnaSort);

            bID = -1;
        }


    }
}


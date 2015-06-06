using UnityEngine;
using System.Collections;

public class WX07_003 : MonoCard {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        crossCip();
        receive();

        heavened();
        ignition();
        
    }

    void crossCip()
    {
        if (sc.CrossNotCip())
            return;

        sc.setDialogNum(DialogNumType.YesNo);
    }

    void receive()
    {
        if (!sc.isMessageYes())
            return;

        sc.setEffect(0, player, Motions.Draw);
        sc.setEffect(0, player, Motions.oneHandDeath);
    }

    void heavened()
    {
        if (sc.mySigniNotHeaven())
            return;

        sc.funcTargetIn(player, Fields.TRASH,check);
        sc.setEffect(-2, 0, Motions.GoHand);
    }

    void ignition()
    {
        if (!sc.Ignition)
            return;
        sc.Ignition = false;

        if (!sc.isCrossOnBattleField(player))
            return;


        sc.Ignition = true;
        if (!sc.IgnitionDown())
            return;

        sc.setEffect(ID, player, Motions.Draw);
        sc.setEffect(ID, player, Motions.Draw);
    }

    bool check(int x, int target)
    {
        return ms.checkType(x, target, cardTypeInfo.スペル);
    }
}


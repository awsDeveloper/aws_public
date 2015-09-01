using UnityEngine;
using System.Collections;

public class WX08_001 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        crossCip();
        heavened();
        ignition();
	
        if(sc.isMessageYes()){
            sc.setPayCost();
            sc.funcTargetIn(player, Fields.SIGNIZONE);
            sc.setEffect(-1,0, Motions.EnAbility);
            sc.addParameta(parametaKey.EnAbilityType, (int)ability.assassin);
        }
	}


    void crossCip()
    {
        if (sc.CrossNotCip())
            return;

        sc.setDialogNum(DialogNumType.YesNo, cardColorInfo.赤, 2);
    }

    void heavened()
    {
        if (sc.mySigniNotHeaven())
            return;

        sc.setEffect(ID,player, Motions.Up);
    }

    void ignition()
    {
        if (!sc.Ignition)
            return;
        sc.Ignition = false;

        if (!sc.isCrossOnBattleField(player))
            return;

        sc.Ignition = true;
        if (!sc.IgnitionDownPayCost(cardColorInfo.赤,1))
            return;

        sc.setEffect(0,1-player, Motions.TopCrash);
    }
}


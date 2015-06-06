using UnityEngine;
using System.Collections;

public class WX07_028 : MonoCard {

    bool bursted_2 = false;

	// Use this for initialization
	void Start () {
        beforeStart();

        var com = gameObject.AddComponent<DialogToggle>();
        com.setTrigger(DialogToggle.triggerType.Burst);
        com.set("ドロー", burst_1);
        com.set("バニッシュ", checkBurst_2, burst_2);
	}
	
	// Update is called once per frame
	void Update () {
        beforeChant();
        chant();

        afterBurst_2();
	}

    bool burst_1()
    {
        sc.setEffect(0, player, Motions.Draw);
        return true;
    }

    bool burst_2()
    {
        sc.setFuncEffect(-1, Motions.HandDeath, player, Fields.HAND, checkChant);
        sc.setEffect(-1, 0, Motions.HandDeath);
        sc.setEffect(-1, 0, Motions.HandDeath);

        bursted_2 = true;

        return true;
    }

    bool checkBurst_2()
    {
        sc.funcTargetIn(player, Fields.HAND, checkChant);
        int c = sc.Targetable.Count;
        sc.Targetable.Clear();

        return c >= 3 && ms.getFieldAllNum((int)Fields.SIGNIZONE, 1 - player) > 0;
    }

    void afterBurst_2()
    {
        if (!bursted_2 || sc.effectTargetID.Count > 0)
            return;

        bursted_2 = false;
        sc.Targetable.Clear();

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        for (int i = 0; i < 2 && i < sc.Targetable.Count; i++)
            sc.setEffect(-1, 0, Motions.EnaCharge);
    }

    void chant()
    {
        if (!sc.isChanted())
            return;
        sc.setFuncEffect(-2, Motions.GoHand, player, Fields.MAINDECK, checkChant);
        for (int i = 0; i < 2 && i<sc.Targetable.Count-1; i++)
            sc.setEffect(-2, 0, Motions.GoHand);

        sc.GUIcancelEnable = true;
    }

    bool checkChant(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精羅_原子);
    }

    void beforeChant()//専用の関数あり
    {
        if(!sc.chantEffectFlag)
            return;

        sc.chantEffectFlag=false;

        sc.funcTargetIn(player, Fields.SIGNIZONE, checkBefore);
        sc.setSystemCardFromCard(-1, Motions.Down, sc.Targetable.Count, sc.Targetable, true, inputReturn);
        sc.Targetable.Clear();
    }

    bool inputReturn(int count)
    {
        for (int i = 0; i < count; i++)
            ms.setSpellCostDown((int)cardColorInfo.青, 2, player, 0);

        return true;
    }

    bool checkBefore(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精羅_原子) && ms.getIDConditionInt(x, target) == (int)Conditions.Up;
    }
}


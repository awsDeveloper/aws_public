using UnityEngine;
using System.Collections;

public class SP03_009 : MonoCard{
    bool afterEffect = false;

	// Use this for initialization
	void Start ()
	{
		beforeStart();

        sc.attackArts = true;

        DialogToggle d = gameObject.AddComponent<DialogToggle>();
        d.setTrigger(DialogToggle.triggerType.Chant);
        d.set("サルベージ", check_1, effect_1);
        d.set("蘇生", effect_2);
	}

	// Update is called once per frame
    void Update()
    {
        after();
    }

    bool check_1()
    {
        return ms.getFieldAllNum((int)Fields.TRASH, player) > 0;
    }

    bool hantei(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精像_悪魔);
    }

    bool effect_1()
    {
        sc.funcTargetIn(player, Fields.TRASH, hantei);

        sc.GUIcancelEnable = true;
        sc.setEffect(-2, 0, Motions.GoHand);
        if (sc.Targetable.Count >= 2)
            sc.setEffect(-2, 0, Motions.GoHand);
        return true;
    }

    bool effect_2()
    {
        sc.setEffect(0, player, Motions.AllHandDeath);
        afterEffect = true;
        return true;
    }

    void after()
    {
        if (!afterEffect || sc.effectTargetID.Count > 0)
            return;

        afterEffect = false;

        sc.funcTargetIn(player, Fields.TRASH, hantei);

        sc.GUIcancelEnable = true;
        sc.setEffect(-2, 0, Motions.Summon);
        if (sc.Targetable.Count >= 2)
            sc.setEffect(-2, 0, Motions.Summon);
    }
}

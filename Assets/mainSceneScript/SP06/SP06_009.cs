using UnityEngine;
using System.Collections;

public class SP06_009 : MonoCard{

    bool chantInput=false;

    int returnNun = -1;
    int levelSum = 0;
    // Use this for initialization
	void Start ()
	{
		beforeStart();

	}

	// Update is called once per frame
	void Update ()
	{
        chant();

        inputReturn();

        banish();
	}

    void chant()
    {
        if (!sc.isChanted())
            return;

        sc.funcTargetIn(player, Fields.TRASH, hantei);
        ms.targetableSameNameRemove(sc);

        if (sc.Targetable.Count == 0)
            return;

        chantInput = true;
        sc.GUIcancelEnable = true;
        for (int i = 0; i < sc.Targetable.Count; i++)
            sc.setEffect(-2, 0, Motions.GoDeckBottom);
    }

    bool hantei(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精羅_原子);
    }

    void inputReturn()
    {
        if (!chantInput || sc.inputReturn == -1)
            return;

        returnNun = sc.inputReturn;

        chantInput = false;
        sc.GUIcancelEnable = false;
        sc.inputReturn = -1;

    }

    void banish()
    {
        if (returnNun < 0 || sc.effectTargetID.Count>0)
            return;

        if (sc.TargetID.Count > 0)
        {
            int tID=sc.TargetID[0];
            sc.TargetID.Clear();

            levelSum += ms.getCardLevel(tID % 50, tID / 50);
        }

        sc.Targetable.Clear();
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE, check);

        if (sc.Targetable.Count == 0)
        {
            returnNun = -1;
            levelSum = 0;
            sc.TargetIDEnable = false;

            sc.setEffect(0, player, Motions.Shuffle);
            return;
        }

        sc.TargetIDEnable = true;
        sc.setEffect(-1, 0, Motions.EnaCharge);
    }

    bool check(int x, int target)
    {
        return ms.getCardLevel(x, target) + levelSum <= returnNun;
    }
}

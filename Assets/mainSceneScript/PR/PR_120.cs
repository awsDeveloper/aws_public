using UnityEngine;
using System.Collections;

public class PR_120 : MonoCard {
    bool afterChantFlag = false;
    bool afafChantFlag = false;

    bool[] effectFlags = new bool[3];

	// Use this for initialization
	void Start () {
        beforeStart();
	}
	
	// Update is called once per frame
	void Update () {
        chant();

        afterChant();

        afafChant();

        effect_0();
        effect_1();
        effect_2();
	}

    void effect_0()
    {
        if (!effectFlags[0] || sc.effectTargetID.Count > 0)
            return;

        effectFlags[0] = false;
        sc.Targetable.Clear();

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE, hantei_0);
        sc.setEffect(-1, 0, Motions.EnaCharge);
    }

    bool hantei_0(int x,int target)
    {
        return ms.getCardPower(x, target) <= 10000;
    }

    void effect_1()
    {
        if (!effectFlags[1] || sc.effectTargetID.Count > 0)
            return;

        effectFlags[1] = false;
        sc.Targetable.Clear();

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE, hantei_1);
        sc.setEffect(-1, 0, Motions.EnaCharge);
    }

    bool hantei_1(int x, int target)
    {
        return ms.getCardPower(x, target) >= 12000;
    }

    void effect_2()
    {
        if (!effectFlags[2] || sc.effectTargetID.Count > 0)
            return;

        effectFlags[2] = false;
        sc.Targetable.Clear();

        sc.setEffect(0, player, Motions.Draw);
    }

    void chant()
    {
        if (!sc.isChanted())
            return;

        sc.funcTargetIn(player, Fields.TRASH, check_1);
        sc.setEffect(-2, 0, Motions.GoHand);
        afterChantFlag = true;
        sc.TargetIDEnable = true;
    }

    void afterChant()
    {
        if (!afterChantFlag || sc.effectTargetID.Count > 0)
            return;

        afterChantFlag = false;
        sc.Targetable.Clear();

        sc.funcTargetIn(player, Fields.TRASH, check_2);
        sc.setEffect(-2, 0, Motions.GoHand);
        afafChantFlag = true;
    }

    void afafChant()
    {
        if (!afafChantFlag || sc.effectTargetID.Count > 0 || sc.TargetID.Count==0)
            return;

        afafChantFlag = false;

        sc.TargetIDEnable = false;
        sc.Targetable.Clear();

        for (int i = 0; i < sc.TargetID.Count; i++)
        {
            int x = sc.TargetID[i] % 50;
            int target = sc.TargetID[i] / 50;

            if(ms.checkColor(x,target, cardColorInfo.赤))
                effectFlags[0]=true;

            if (ms.checkColor(x, target, cardColorInfo.緑))
                effectFlags[1] = true;

            if (ms.checkColor(x, target, cardColorInfo.青))
                effectFlags[2] = true;
        }

        sc.TargetID.Clear();
    }

    bool check_1(int x, int target)
    {
        return ms.checkType(x, target, cardTypeInfo.シグニ);
    }

    bool check_2(int x, int target)
    {
        return ms.checkType(x, target, cardTypeInfo.スペル);
    }
}

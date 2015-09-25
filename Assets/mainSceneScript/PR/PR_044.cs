using UnityEngine;
using System.Collections;

public class PR_044 : MonoCard {
	// Use this for initialization
	void Start () {
        sc.attackArts = true;

        var com= gameObject.AddComponent<DialogToggle>();
        com.setTrigger(DialogToggle.triggerType.Chant);
        com.setAction("デッキデス", effect_1);
        com.setAction("蘇生", effect_2);
        com.setAction("パワーダウン", effect_3);		
	}

	// Update is called once per frame
	void Update () {
	}

    void effect_1()
    {
        int target = player;
        int f = 0;
        int num = ms.getFieldAllNum(f, target);

        for (int i = 0; i < num && i < 7; i++)
        {
            int x = ms.getFieldRankID(f, num - 1 - i, target);
            if (x >= 0)
            {
                sc.effectFlag = true;
                sc.effectTargetID.Add(x + 50 * target);
                sc.effectMotion.Add(7);
            }
        }

        target = 1 - player;
        num = ms.getFieldAllNum(f, target);

        for (int i = 0; i < num && i < 7; i++)
        {
            int x = ms.getFieldRankID(f, num - 1 - i, target);
            if (x >= 0)
            {
                sc.effectFlag = true;
                sc.effectTargetID.Add(x + 50 * target);
                sc.effectMotion.Add(7);
            }
        }
    }

    void effect_2()
    {
        int target = player;
        int f = 7;
        int num = ms.getFieldAllNum(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if (x >= 0 && ms.getCardLevel(x, target) <= 3 && ms.checkClass(x, target, cardClassInfo.精械_古代兵器))
            {
                sc.Targetable.Add(x + 50 * target);
            }
        }

        if (sc.Targetable.Count > 0)
        {
            sc.effectFlag = true;
            sc.effectTargetID.Add(-2);
            sc.effectMotion.Add(6);
        }
    }

    void effect_3()
    {
        if (ms.getFieldAllNum(7, player) >= 20) sc.powerUpValue = -8000;
        else if (ms.getFieldAllNum(7, player) >= 10) sc.powerUpValue = -5000;
        else sc.powerUpValue = 0;

        for (int i = 0; i < 3; i++)
        {
            int x = ms.getFieldRankID(3, i, 1 - player);
            if (x >= 0)
            {
                sc.effectFlag = true;
                sc.effectTargetID.Add(x + 50 * (1 - player));
                sc.effectMotion.Add(34);
            }
        }
    }
	
}

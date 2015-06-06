using UnityEngine;
using System.Collections;

public class WX07_017 : MonoCard {
    bool[] flags = new bool[7];
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.setFieldAllEffect(player, Fields.HAND, Motions.GoTrash);
            sc.setFieldAllEffect(1 - player, Fields.HAND, Motions.GoTrash);

            sc.setFieldAllEffect(player, Fields.ENAZONE, Motions.GoTrash);
            sc.setFieldAllEffect(1 - player, Fields.ENAZONE, Motions.GoTrash);

            sc.setFieldAllEffect(player, Fields.SIGNIZONE, Motions.GoTrash);
            sc.setFieldAllEffect(1 - player, Fields.SIGNIZONE, Motions.GoTrash);

            flags[0] = true;
        }

        effect(0, Motions.Summon);
        effect(2, Motions.GoHand);
        effect(4, Motions.GoEnaZone);

/*        for (int i = 0; i < 2; i++)
        {
            if (flags[i+1] && sc.effectTargetID.Count == 0)
            {
                flags[i+1] = false;

                sc.GUIcancelEnable = true;
                sc.setFuncEffect(-2, Motions.Summon, Mathf.Abs(i-player), Fields.TRASH, check);
                flags[i+2] = true;
            }
        }

        for (int i = 0; i < 2; i++)
        {
            if (flags[i + 3] && sc.effectTargetID.Count == 0)
            {
                flags[i + 3] = false;

                sc.GUIcancelEnable = true;
                sc.setFuncEffect(-2, Motions.GoHand, Mathf.Abs(i - player), Fields.TRASH, check);
                flags[i + 4] = true;
            }
        }

        for (int i = 0; i < 2; i++)
        {
            if (flags[i + 5] && sc.effectTargetID.Count == 0)
            {
                flags[i + 5] = false;

                sc.GUIcancelEnable = true;
                sc.setFuncEffect(-2, Motions.GoEnaZone, Mathf.Abs(i - player), Fields.TRASH, check);
                flags[i + 6] = true;
            }
        }
*/	
	}

    bool check(int x, int target)
    {
        return ms.checkType(x, target, cardTypeInfo.シグニ);
    }

    void effect(int hosei, Motions m)
    {
        for (int i = 0; i < 2; i++)
        {
            if (flags[i + hosei] && sc.effectTargetID.Count == 0)
            {
                flags[i + hosei] = false;
                sc.Targetable.Clear();

                int target = Mathf.Abs(i - player);

                sc.GUIcancelEnable = true;
                sc.effectSelecter = target;

                for (int k = 0; k < 3; k++)
                    sc.setFuncEffect(-2, m, target, Fields.TRASH, check);

                flags[i + hosei+1] = true;
            }
        }

    }
}


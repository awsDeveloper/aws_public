using UnityEngine;
using System.Collections;

public class BanishedSearch : MonoCard {
    colorCostArry myCost;
    string searchNane = "";

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //moyasi
        if (ms.getBanishedID() == ID + player * 50)
        {
            sc.changeCost(myCost);

            if (ms.checkCost(ID, player))
                sc.DialogFlag = true;
        }


        //receive
        if (sc.messages.Count > 0)
        {
            if (sc.messages[0].Contains("Yes") && ms.checkCost(ID, player))
            {
                sc.effectFlag = true;
                sc.effectTargetID.Add(ID + 50 * player);
                sc.effectMotion.Add(17);

                int target = player;
                int f = 0;
                int num = ms.getNumForCard(f, target);

                for (int i = num - 1; i >= 0; i--)
                {
                    int x = ms.getFieldRankID(f, i, target);
                    if (x >= 0 && ms.checkName(x,target,searchNane))
                        sc.Targetable.Add(x + 50 * target);
                }

                if (sc.Targetable.Count > 0)
                {
                    sc.effectTargetID.Add(-2);
                    sc.effectMotion.Add(16);
                }
            }

 
            sc.messages.Clear();
        }

        //burst
        if (sc.isBursted())
        {
            sc.effectFlag = true;
            sc.effectTargetID.Add(50 * player);
            sc.effectMotion.Add(2);
        }

    }

    public void set(colorCostArry array, string targetName)
    {
        myCost = array;
        searchNane = targetName;
    }
}


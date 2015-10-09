using UnityEngine;
using System.Collections;

public class WD07_001 : MonoBehaviour
{
    GameObject Manager;
    DeckScript ManagerScript;
    GameObject Body;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool costFlag = false;

    // Use this for initialization
    void Start()
    {
        Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;
        BodyScript.powerUpValue = -2000;

        Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        gameObject.AddComponent<FuncPowerUp>().set(-2000, check, null, 1 - player);
    }

    // Update is called once per frame
    void Update()
    {
        ManagerScript.upAlwaysFlag(alwysEffs.Iona, 1 - player, ID, player);

        if (ID == ManagerScript.getLrigID(player) && BodyScript.Ignition)
        {

            BodyScript.Ignition = false;

            BodyScript.Cost[0] = 0;
            BodyScript.Cost[1] = 0;
            BodyScript.Cost[2] = 0;
            BodyScript.Cost[3] = 0;
            BodyScript.Cost[4] = 0;
            BodyScript.Cost[5] = 1;

            if (ManagerScript.checkCost(ID, player) && checkCost())
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(ID + player * 50);
                BodyScript.effectMotion.Add(17);
                BodyScript.effectTargetID.Add(46 * 100 + 1 + 50 * player);
                BodyScript.effectMotion.Add(46);
                BodyScript.effectTargetID.Add(46 * 100 + 5 + 50 * player);
                BodyScript.effectMotion.Add(46);

                costFlag = true;
            }
        }

        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;
            BodyScript.Targetable.Clear();

            int target = 1 - player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0)
                {
                    BodyScript.Targetable.Add(x + 50 * target);
                }
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(5);
            }
        }
    }

    bool checkCost()
    {
        int target = player;
        int f = 2;
        int num = ManagerScript.getNumForCard(f, target);

        bool flag_w = false;
        bool flag_b = false;

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0 && ManagerScript.getCardType(x, target) == 2)
            {
                switch (ManagerScript.getCardColor(x, target))
                {
                    case 1:
                        flag_w = true;
                        break;
                    case 5:
                        flag_b = true;
                        break;
                }
            }
        }
        return flag_b && flag_w;
    }

    bool check()
    {
        return ID == ManagerScript.getLrigID(player) && ManagerScript.isColorSigni(1, player) && ManagerScript.isColorSigni(5, player);
    }
}

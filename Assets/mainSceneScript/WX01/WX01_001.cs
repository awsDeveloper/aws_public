using UnityEngine;
using System.Collections;

public class WX01_001 : MonoBehaviour
{
    //WX03_001 ウルム参照
    GameObject Manager;
    DeckScript ManagerScript;
    GameObject Body;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool costFlag = false;
    bool IgnitionFlag = false;

    // Use this for initialization
    void Start()
    {
        Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;
        Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();
    }

    // Update is called once per frame
    void Update()
    {
        int lrigDeckNum = ManagerScript.getFieldAllNum(4, player);
        if (ID == ManagerScript.getFieldRankID(4, lrigDeckNum - 1, player))
        {
            if (BodyScript.Ignition && !IgnitionFlag)
            {
                int handNum = ManagerScript.getFieldAllNum(2, player);
                int costNum = ManagerScript.getEnaColorNum(1, player);
                if (costNum == 0) costNum = ManagerScript.MultiEnaNum(player);
                for (int i = 0; i < handNum; i++)
                {
                    int x = ManagerScript.getFieldRankID(2, i, player);
                    if (x > 0 && ManagerScript.getCardColor(x, player) == 1 && ManagerScript.getCardType(x, player) == 2)
                    {
                        BodyScript.Targetable.Add(x + 50 * player);
                    }
                }
                if (BodyScript.Targetable.Count >= 1 && costNum >= 1)
                {
                    costFlag = true;
                    BodyScript.effectFlag = true;
                    BodyScript.effectTargetID.Add(-1);
                    BodyScript.effectMotion.Add(19);
                    BodyScript.effectTargetID.Add(ID + player * 50);
                    BodyScript.effectMotion.Add(17);
                    BodyScript.Cost[0] = 0;
                    BodyScript.Cost[1] = 1;
                    BodyScript.Cost[2] = 0;
                    BodyScript.Cost[3] = 0;
                    BodyScript.Cost[4] = 0;
                    BodyScript.Cost[5] = 0;
                }
                else BodyScript.Ignition = false;
            }
        }

        if (BodyScript.effectTargetID.Count == 0 && costFlag)
        {
            costFlag = false;
            BodyScript.Ignition = false;
            BodyScript.Targetable.Clear();

            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, 1 - player);
                if (x > 0)
                {
                    BodyScript.Targetable.Add(x + (1 - player) * 50);
                }
            }
            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(16);
            }
        }
        IgnitionFlag = BodyScript.Ignition;
    }

}

using UnityEngine;
using System.Collections;

public class PR_110 : MonoBehaviour
{
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool upFlag = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();

        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ManagerScript.getFieldInt(ID, player) == 3 && check())
        {
            if (!upFlag)
            {
                ManagerScript.changeBasePower(ID, player, 15000);
                BodyScript.resiEffect = true;
                upFlag = true;
            }
        }
        else if (upFlag)
        {
            ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower);
            BodyScript.resiEffect = false;
            upFlag = false;
        }

        burst();
    }

    void burst()
    {
        if (!BodyScript.isBursted())
            return;

        BodyScript.funcTargetIn(player, Fields.MAINDECK, burstCheck);

        BodyScript.setEffect(-2, 0, Motions.GoHand);
    }

    bool burstCheck(int x,int target)
    {
        return ManagerScript.checkType(x,target, cardTypeInfo.シグニ) && ManagerScript.getCardLevel(x, target) <= 3;
    }

    bool check()
    {
        if (ManagerScript.getFieldAllNum((int)Fields.SIGNIZONE, player) != 3)
            return false;

        foreach (var item in System.Enum.GetValues(typeof(cardColorInfo)))
        {
            cardColorInfo info = (cardColorInfo)item;
            int count=0;

            if (info != cardColorInfo.無色)
            {
                int target = player;
                int f = (int)Fields.SIGNIZONE;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);
                    if (x >= 0 && ManagerScript.checkColor(x, target, info))
                        count++;
                }
            }

            if (count >= 2)
                return false;
        }

        return true;
    }
}
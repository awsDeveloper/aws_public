using UnityEngine;
using System.Collections;

public class WX04_038 : MonoBehaviour
{
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;
        BodyScript.powerUpValue = 1000;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        //dialog
        BodyScript.checkStr.Add("サルベージ");
        BodyScript.checkStr.Add("蘇生");

        for (int i = 0; i < BodyScript.checkStr.Count; i++)
        {
            BodyScript.checkBox.Add(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //chant
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add(56);
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            targetIN();

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.Targetable.Clear();

                BodyScript.DialogFlag = true;
                BodyScript.DialogNum = 2;
                BodyScript.DialogCountMax = 1;
            }
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                effect_1();
                effect_2();
            }

            BodyScript.messages.Clear();
        }

        //UpDate
        field = ManagerScript.getFieldInt(ID, player);
    }

    void effect_1()
    {
        if (BodyScript.checkBox[0])
        {
            targetIN();

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-2);
                BodyScript.effectMotion.Add(16);
            }
        }
    }

    void effect_2()
    {
        if (BodyScript.checkBox[1])
        {
            targetIN();

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-2);
                BodyScript.effectMotion.Add(6);
            }
        }
    }

    void targetIN()
    {
        int target = player;
        int f = 7;
        int num = ManagerScript.getFieldAllNum(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if (x >= 0 && ManagerScript.getSigniColor(x, target) == 5)
            {
                BodyScript.Targetable.Add(x + 50 * target);
            }
        }
    }
}
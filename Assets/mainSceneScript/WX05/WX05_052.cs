using UnityEngine;
using System.Collections;

public class WX05_052 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;
    bool chantFlag = false;
    bool effectFlag = false;
    bool effectFlag_2 = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.checkStr.Add("白シグニ");
        BodyScript.checkStr.Add("シグニ");

        for (int i = 0; i < BodyScript.checkStr.Count; i++)
        {
            BodyScript.checkBox.Add(false);

        }

    }
    // Update is called once per frame
    void Update()
    {
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            BodyScript.DialogFlag = true;
            BodyScript.DialogCountMax = 2;
            BodyScript.DialogNum = 2;
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

        if (!BodyScript.DialogFlag)
        {
            BodyScript.Targetable.Clear();

            if (effectFlag && BodyScript.effectTargetID.Count == 0)
            {
                effectFlag = false;

                if (!BodyScript.AntiCheck)
                    effectSetting(true);
                else
                    BodyScript.AntiCheck = false;
            }

            if (effectFlag_2 && BodyScript.effectTargetID.Count == 0)
            {
                effectFlag_2 = false;

                if (!BodyScript.AntiCheck)
                    effectSetting(false);
                else
                    BodyScript.AntiCheck = false;
            }
        }

        field = ManagerScript.getFieldInt(ID, player);
    }

    void effectSetting(bool isNumber_1)
    {
        int num = ManagerScript.getFieldAllNum(0, player);
        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(0, i, player);
            if (check(x, player, isNumber_1))
                BodyScript.Targetable.Add(x + 50 * player);
        }

        if (BodyScript.Targetable.Count > 0)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(-2);
            BodyScript.effectMotion.Add(16);
        }
    }

    bool check(int x, int target, bool isNumber_1)
    {
        if (x < 0 || ManagerScript.getCardType(x, player) != 2)
            return false;

        if (!isNumber_1)
            return true;

        return ManagerScript.getCardLevel(x, player) <= 2 && ManagerScript.getCardColor(x, player) == 1;
    }

    void effect_1()
    {
        if (!BodyScript.checkBox[0])
            return;

        BodyScript.setAntiCheck();
        effectFlag = true;
    }

    void effect_2()
    {
        if (!BodyScript.checkBox[1])
            return;

        if (ManagerScript.getLrigColor(player) != 1 || ManagerScript.getLrigLevel(player) < 4)
            return;

        BodyScript.setAntiCheck();
        effectFlag_2 = true;
      }
}

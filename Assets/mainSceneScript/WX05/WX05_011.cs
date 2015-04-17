using UnityEngine;
using System.Collections;

public class WX05_011 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool costFlag_1 = false;
    bool costFlag_2 = false;

    int exCost = 2;
    bool waitFlag = false;

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

        //dialog
        BodyScript.checkStr.Add("並べ替え");
        BodyScript.checkStr.Add("墓地スペル詠唱");

        for (int i = 0; i < BodyScript.checkStr.Count; i++)
        {
            BodyScript.checkBox.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //useLim
        int nowLrigID = ManagerScript.getLrigID(player);
        if (nowLrigID >= 0)
            BodyScript.useLimit = !ManagerScript.getCardScr(nowLrigID, player).Name.Contains("ミルルン");

        if (ManagerScript.getLrigID(player) == ID)
        {
            ManagerScript.mirutychoFlag[1 - player] = true;
            upFlag = true;
        }
        else if (upFlag)
        {
            upFlag = false;
            ManagerScript.mirutychoFlag[1 - player] = false;
        }

        //ignition
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            ManagerScript.targetableExceedIn(player, BodyScript);

            int count = BodyScript.Targetable.Count;
            BodyScript.Targetable.Clear();

            if (count >= exCost)
            {
                BodyScript.Targetable.Clear();

                BodyScript.DialogFlag = true;
                BodyScript.DialogNum = 2;
                BodyScript.DialogCountMax = 1;
            }
            else if (count >= exCost)
                effect_2();
            else if (count >= 1)
                effect_1();

        }

        //ignition after cost
        if (costFlag_1 && BodyScript.effectTargetID.Count == 0)
        {
            costFlag_1 = false;

            if (ManagerScript.getLastMotionsRear() == (int)Motions.Exceed)
            {
                BodyScript.Targetable.Clear();

                int target = player;
                int f = (int)Fields.MAINDECK;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < 5 && num-1-i>=0; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, num-1-i, target);
                    if (x >= 0)
                        BodyScript.Targetable.Add(x + 50 * target);
                }

                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.effectFlag = true;
                    BodyScript.GUIcancelEnable = true;
                    waitFlag = true;

                    for (int i = 0; i < 5 && i<BodyScript.Targetable.Count; i++)
                    {
                        BodyScript.effectMotion.Add((int)Motions.GoDeckBottom);
                        BodyScript.effectTargetID.Add(-2);
                    }
               }
            }
        }

        //after wait
        if (waitFlag && BodyScript.effectTargetID.Count == 0)
        {
            waitFlag = false;
            BodyScript.GUIcancelEnable = false;

            for (int i = 0; i < BodyScript.Targetable.Count; i++)
            {
                BodyScript.effectTargetID.Add(BodyScript.Targetable[i]);
                BodyScript.effectMotion.Add((int)Motions.CheckBack);
            }

            BodyScript.Targetable.Clear();
        }

        //ignition after cost_2
        if (costFlag_2 && BodyScript.effectTargetID.Count == 0)
        {
            costFlag_2 = false;
            BodyScript.Targetable.Clear();

            if (ManagerScript.getLastMotionsRear() == (int)Motions.Exceed)
            {
                int target = 1 - player;
                int f = (int)Fields.TRASH;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);

                    if (x >= 0 && ManagerScript.getCardType(x, target) == 3)
                        BodyScript.Targetable.Add(x + 50 * target);
                }

                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.effectFlag = true;
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add(68);
                }
            }
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                if (BodyScript.checkBox[0]) effect_1();
                else if (BodyScript.checkBox[1]) effect_2();
            }

            BodyScript.messages.Clear();
        }
    }

    void effect_1()
    {
        costFlag_1 = true;
        ManagerScript.targetableExceedIn(player, BodyScript);
        BodyScript.effectFlag = true;
        BodyScript.effectTargetID.Add(-2);
        BodyScript.effectMotion.Add((int)Motions.Exceed);
    }

    void effect_2()
    {
        costFlag_2 = true;

        ManagerScript.targetableExceedIn(player, BodyScript);
        BodyScript.effectFlag = true;

        for (int i = 0; i < exCost; i++)
        {
            BodyScript.effectTargetID.Add(-2);
            BodyScript.effectMotion.Add((int)Motions.Exceed);
        }
    }
}

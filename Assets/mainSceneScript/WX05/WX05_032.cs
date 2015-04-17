using UnityEngine;
using System.Collections;

public class WX05_032 : MonoBehaviour
{
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool DialogFlag = false;
    bool costFlag_1 = false;
    bool costFlag_2 = false;

    int field = -1;

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
        BodyScript.checkStr.Add("バニッシュ");
        BodyScript.checkStr.Add("クラッシュ");

        for (int i = 0; i < BodyScript.checkStr.Count; i++)
            BodyScript.checkBox.Add(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            bool[] flag = new bool[2];
            int target = 1 - player;
            int[] count = new int[2] { 0, 0 };

            handTargetIN();
            int handCostNum = BodyScript.Targetable.Count;
            BodyScript.Targetable.Clear();

            //ignition check 1
            banishTargetIN();

            if (handCostNum >= 1 && BodyScript.Targetable.Count > 0)
                flag[0] = true;

            BodyScript.Targetable.Clear();

            //ignition check 2
            if (handCostNum >= 3 && ManagerScript.getFieldAllNum((int)Fields.LIFECLOTH, 1 - player) > 0)
                flag[1] = true;
 
            //select root
            if (flag[0] && flag[1])
            {
                BodyScript.DialogFlag = true;
                BodyScript.DialogNum = 2;
                BodyScript.DialogCountMax = 1;
            }
            else if (flag[0])
            {
                effect_1();
            }
            else if (flag[1])
            {
                effect_2();
            }

        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                if (BodyScript.checkBox[0])
                    effect_1();
                else if (BodyScript.checkBox[1])
                    effect_2();
            }

            BodyScript.messages.Clear();
        }

        //ignition 1 after cost
        if (costFlag_1 && BodyScript.effectTargetID.Count == 0)
        {
            costFlag_1 = false;

            if (ManagerScript.getLastMotionsRear() == (int)Motions.HandDeath)
            {
                banishTargetIN();

                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.effectTargetID.Add(-1);
                    BodyScript.effectMotion.Add(5);
                }
            }
        }

        //ignition 2 after cost
        if (costFlag_2 && BodyScript.effectTargetID.Count == 0)
        {
            costFlag_2 = false;

            if (ManagerScript.getLastMotionsRear() == (int)Motions.HandDeath)
            {
                BodyScript.effectTargetID.Add(50 * (1 - player));
                BodyScript.effectMotion.Add((int)Motions.NotBurstTopCrash);
            }
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            burstTargetIN();
            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectMotion.Add(5);
                BodyScript.effectTargetID.Add(-1);
            }
        }

        //update
        field = ManagerScript.getFieldInt(ID, player);
    }

    void effect_1()
    {
        costFlag_1 = true;
        handTargetIN();

        if (BodyScript.Targetable.Count > 0)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(-1);
            BodyScript.effectMotion.Add((int)Motions.HandDeath);
        }
    }

    void effect_2()
    {
        costFlag_2 = true;
        handTargetIN();

        if (BodyScript.Targetable.Count >= 3)
        {
            BodyScript.effectFlag = true;

            for (int i = 0; i < 3; i++)
            {
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add((int)Motions.HandDeath);

            }
        }
        else
            BodyScript.Targetable.Clear();
    }

    void handTargetIN()
    {
        int target = player;
        int f = 2;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if(x>=0 && checkClass(x,target))
                BodyScript.Targetable.Add(x+50*target);
        }
    }

    void banishTargetIN()
    {
        int target=1-player;
        for (int i = 0; i < 3; i++)
        {
            int x = ManagerScript.getFieldRankID(3, i, target);
            if (x >= 0 && ManagerScript.getCardPower(x, target) <= 8000)
                BodyScript.Targetable.Add(x + 50 * target);
        }

    }

    void burstTargetIN()
    {
        int target = 1 - player;
        for (int i = 0; i < 3; i++)
        {
            int x = ManagerScript.getFieldRankID(3, i, target);
            if (x >= 0 && ManagerScript.getCardPower(x, target) <= 10000)
                BodyScript.Targetable.Add(x + 50 * target);
        }

    }

    bool checkClass(int x, int cplayer)
    {
        return ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_ウェポン);
    }

}

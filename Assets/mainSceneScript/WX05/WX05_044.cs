using UnityEngine;
using System.Collections;

public class WX05_044 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool DialogFlag = false;
    bool costFlag_1 = false;
    bool costFlag_2 = false;

    bool costFlag = false;

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
        BodyScript.checkStr.Add("単体");
        BodyScript.checkStr.Add("全体");

        for (int i = 0; i < BodyScript.checkStr.Count; i++)
            BodyScript.checkBox.Add(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            if (ManagerScript.getIDConditionInt(ID, player) == (int)Conditions.Up)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectMotion.Add((int)Motions.Down);
                BodyScript.effectTargetID.Add(ID + 50 * player);

                costFlag = true;
            } 
        }

        //after down
        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;

            if (ManagerScript.getLastMotionsRear() == (int)Motions.Down)
            {
                bool[] flag = new bool[2];
                int target = 1 - player;
                int[] count = new int[2] { 0, 0 };

                costTargetIN();
                int costNum = BodyScript.Targetable.Count;
                BodyScript.Targetable.Clear();

                //ignition check 1
                effectTargetIN();

                if (costNum >= 1 && BodyScript.Targetable.Count > 0)
                    flag[0] = true;

                //ignition check 2
                if (costNum >= 2 && BodyScript.Targetable.Count > 0)
                    flag[1] = true;

                BodyScript.Targetable.Clear();

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

            if (ManagerScript.getLastMotionsRear() == (int)Motions.EnaCharge)
            {
                effectTargetIN();

                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.powerUpValue = -7000;

                    BodyScript.effectTargetID.Add(-1);
                    BodyScript.effectMotion.Add((int)Motions.PowerUpEndPhase);
                }
            }
        }

        //ignition 2 after cost
        if (costFlag_2 && BodyScript.effectTargetID.Count == 0)
        {
            costFlag_2 = false;

            if (ManagerScript.getLastMotionsRear() == (int)Motions.CostGoTrash)
            {
                effectTargetIN();

                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.powerUpValue = -10000;

                    for (int i = 0; i < BodyScript.Targetable.Count; i++)
                    {
                        BodyScript.effectTargetID.Add( BodyScript.Targetable[i] );
                        BodyScript.effectMotion.Add((int)Motions.PowerUpEndPhase);                        
                    }

                    BodyScript.Targetable.Clear();
                }
            }
        }
    }

    void effect_1()
    {
        costFlag_1 = true;
        costTargetIN();

        if (BodyScript.Targetable.Count > 0)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(-1);
            BodyScript.effectMotion.Add((int)Motions.EnaCharge);
        }
    }

    void effect_2()
    {
        costFlag_2 = true;
        costTargetIN();

        if (BodyScript.Targetable.Count >= 2)
        {
            BodyScript.effectFlag = true;

            for (int i = 0; i < 2; i++)
            {
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add((int)Motions.CostGoTrash);
            }
        }
        else
            BodyScript.Targetable.Clear();
    }

    void costTargetIN()
    {
        int target = player;
        int f = 3;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if (x >= 0 && checkClass(x, target) && x != ID)
                BodyScript.Targetable.Add(x + 50 * target);
        }
    }

    void effectTargetIN()
    {
        int target = 1 - player;
        for (int i = 0; i < 3; i++)
        {
            int x = ManagerScript.getFieldRankID(3, i, target);
            if (x >= 0)
                BodyScript.Targetable.Add(x + 50 * target);
        }

    }

    bool checkClass(int x, int target)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, target);
        return (c[0] == 3 && c[1] == 1);
    }
}

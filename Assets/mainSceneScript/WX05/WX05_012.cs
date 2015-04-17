using UnityEngine;
using System.Collections;

public class WX05_012 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool[] rootFlag = new bool[4];

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.attackArts = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            int target = player;
            int f = 0;
            int num = ManagerScript.getNumForCard(f, target);
            int count = 0;

            for (int i = num - 1; i > num - 1 - 5; i--)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0)
                {
                    //count name
                    if (checkClass(x, target))
                    {
                        string name = ManagerScript.getCardScr(x, target).Name;
                        bool flag = false;

                        for (int j = i - 1; j > num - 1 - 5; j--)
                        {
                            int y = ManagerScript.getFieldRankID(f, j, target);

                            if (name == ManagerScript.getCardScr(y, target).Name)
                            {
                                flag = true;
                                break;
                            }
                        }

                        if (!flag)
                            count++;
                    }

                    //show targetIn
                    BodyScript.effectFlag = true;
                    BodyScript.effectTargetID.Add(x + 50 * target);
                    BodyScript.effectMotion.Add(54);
                }
            }

            if (count >= 3)
                rootFlag[0] = true;

            if (count >= 4)
                rootFlag[1] = true;

            if (count >= 5)
                rootFlag[2] = true;

            rootFlag[3] = true;
        }

        //root 0
        if (rootFlag[0] && BodyScript.effectTargetID.Count == 0)
        {
            rootFlag[0] = false;

            int target = 1 - player;
            int f = (int)Fields.SIGNIZONE;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add((int)Motions.DownAndFreeze);
            }

        }

        //root 1
        if (rootFlag[1] && BodyScript.effectTargetID.Count == 0)
        {
            rootFlag[1] = false;
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add((int)Motions.Draw);
        }

        //root 2
        if (rootFlag[2] && BodyScript.effectTargetID.Count == 0)
        {
            rootFlag[2] = false;

            int target = 1 - player;
            int f = (int)Fields.SIGNIZONE;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add((int)Motions.EnaCharge);
            }

        }

        //root 3
        if (rootFlag[3] && BodyScript.effectTargetID.Count == 0)
        {
            rootFlag[3] = false;

            for (int i = 0;true; i++)
            {
                int x = ManagerScript.getShowZoneID(i);

                if (x < 0)
                    break;

                if (x / 50 == player)
                    BodyScript.Targetable.Add(x);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                for (int i = 0; i < BodyScript.Targetable.Count; i++)
                {
                    BodyScript.effectMotion.Add((int)Motions.GoDeckBottom);
                    BodyScript.effectTargetID.Add(-2);
                }
            }
        }
        //update
        field = ManagerScript.getFieldInt(ID, player);
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0)
            return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[0] == 2 && c[1] == 3);
    }
}

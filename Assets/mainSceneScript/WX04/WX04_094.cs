using UnityEngine;
using System.Collections;

public class WX04_094 : MonoBehaviour
{
    GameObject Manager;
    DeckScript ManagerScript;
    GameObject Body;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    // Use this for initialization
    void Start()
    {
        Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;
        Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.powerUpValue = 2000;
    }

    // Update is called once per frame
    void Update()
    {
        //chant
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            int target = player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0 && checkClass(x, target))
                {
                    BodyScript.Targetable.Add(x + 50 * target);
                }
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(34);

                BodyScript.TargetIDEnable = true;

            }
        }

        if (BodyScript.TargetID.Count > 0)
        {
            if (getClassNum(player) == 3)
            {
                int x = BodyScript.TargetID[0];
                BodyScript.effectTargetID.Add(x);
                BodyScript.effectMotion.Add(62);
            }

            BodyScript.TargetID.Clear();
            BodyScript.TargetIDEnable = false;
        }

        //UpDate
        field = ManagerScript.getFieldInt(ID, player);
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[0] == 5 && c[1] == 1) || (c[0] == 5 && c[1] == 2);
    }

    int getClassNum(int target)
    {
        int num = 0;
        for (int i = 0; i < 3; i++)
        {
            int x = ManagerScript.getFieldRankID(3, i, target);
            if (x >= 0 && checkClass(x, target))
                num++;
        }
        return num;
    }
}

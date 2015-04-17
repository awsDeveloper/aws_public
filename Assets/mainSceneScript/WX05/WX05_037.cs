using UnityEngine;
using System.Collections;

public class WX05_037 : MonoBehaviour
{
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool cFlag = false;

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
        //burst
        if (ManagerScript.getFieldInt(ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
        {
            int target = 1 - player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);

                if (x >= 0)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                cFlag = true;
                BodyScript.setEffect(-1, 0, Motions.FREEZE);
            }
        }

        //after cip
        if (cFlag && BodyScript.effectTargetID.Count == 0)
        {
            cFlag = false;

            int target = 1 - player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            bool flag = true;

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);

                if (x >= 0 && !ManagerScript.getCardScr(x, target).Freeze)
                    flag = false;
            }

            if (flag)
                BodyScript.setEffect(0, target, Motions.RandomHandDeath);
        }

        //update
        field = ManagerScript.getFieldInt(ID, player);
    }
}
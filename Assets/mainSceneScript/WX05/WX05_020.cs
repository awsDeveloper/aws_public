using UnityEngine;
using System.Collections;

public class WX05_020 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool oneceTurn = false;
    int count=0;

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
        //triggered
        if (!oneceTurn && field == 3 && ManagerScript.getTargetNowID() != -1 && ManagerScript.getEffecterNowID() / 50 == 1 - player)
        {
            int tID = ManagerScript.getTargetNowID();
            int eID = ManagerScript.getEffecterNowID();

            int type = ManagerScript.getCardType(eID % 50, eID / 50);
            int f = ManagerScript.getFieldInt(tID % 50, tID / 50);

            if (tID / 50 == player && f == 3 && checkClass(tID % 50, tID / 50) && type == 1)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(50 * (1 - player));
                BodyScript.effectMotion.Add(71);
                oneceTurn = true;
            }
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            int target = 1 - player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);

                if (x >= 0 && ManagerScript.getCardPower(x, target) <= 10000)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(5);

            }

            if (ManagerScript.getFieldAllNum(5, player) <= 2)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(50 * (1 - player));
                BodyScript.effectMotion.Add(51);
            }
        }

        if (ManagerScript.CrashedID != -1)
        {
            int crashed = ManagerScript.CrashedID;
            int crasher = ManagerScript.CrasherID;

            if (crasher == ID + 50 * player)
                count++;

            if (count == 2)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(crasher);
                BodyScript.effectMotion.Add(9);
            }
        }

        //UpDate
        field = ManagerScript.getFieldInt(ID, player);

        if (oneceTurn && ManagerScript.getPhaseInt() == 7)
        {
            oneceTurn = false;
            count = 0;
        }
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return c[0] == 2 && (c[1] == 0 || c[1] == 1);
    }
}
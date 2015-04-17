using UnityEngine;
using System.Collections;

public class WX05_025 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool costFlag = false;
    int etID = -1;

    bool bursted = false;

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
        //moyasi
        if (ManagerScript.getBanishedID()!=-1 && field == 3)
        {
            etID = ManagerScript.getBanishedID();

            if (checkClass(etID % 50, etID / 50) && etID/50 == player)
            {
                BodyScript.Cost[0] = 0;
                BodyScript.Cost[1] = 1;
                BodyScript.Cost[2] = 0;
                BodyScript.Cost[3] = 0;
                BodyScript.Cost[4] = 1;
                BodyScript.Cost[5] = 0;

                if (ManagerScript.checkCost(ID, player))
                    BodyScript.DialogFlag = true;
            }
        }


        //triggered
        if (ManagerScript.StopAttackedID != -1 && ManagerScript.StopAttackedID/50 == 1-player && field == 3)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add((int)Motions.TopEnaCharge);           
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add((int)Motions.TopEnaCharge);

            bursted = true;
        }

        //after burst
        if (bursted && (ManagerScript.LrigAttackNow || (ManagerScript.getAttackerID() != -1 && ManagerScript.getAttackerID() / 50 == 1 - player)))
        {
            bursted = false;

            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add((int)Motions.stopAttack);
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes") && ManagerScript.checkCost(ID, player))
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(ID + 50 * player);
                BodyScript.effectMotion.Add(17);

                costFlag = true;
            }

            BodyScript.messages.Clear();
        }

        //after cost
        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;

            if (ManagerScript.getFieldInt(etID % 50, etID / 50) == (int)Fields.ENAZONE && ManagerScript.getLastMotionsRear() == 17)
            {
                BodyScript.effectTargetID.Add(etID);
                BodyScript.effectMotion.Add((int)Motions.Summon);
            }
        }


        //UpDate
        field = ManagerScript.getFieldInt(ID, player);

        if (bursted && ManagerScript.getPhaseInt() == 7)
            bursted = false;
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0)
            return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[0] == 4 && c[1] == 2);
    }
}

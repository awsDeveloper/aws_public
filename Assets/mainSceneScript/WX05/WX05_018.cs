using UnityEngine;
using System.Collections;

public class WX05_018 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;
    bool costFlag = false;

    bool upFlag = false;
    bool enajeFlag = false;

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
        //enaPhase skip
        if (ManagerScript.getFieldInt(ID, player) == 3)
        {
            upFlag = true;
            ManagerScript.EnaPhaseSkip[1 - player] = true;
        }
        else if (upFlag)
        {
            upFlag = false;
            ManagerScript.EnaPhaseSkip[1 - player] = false;
        }

        //enaje
        if (alwaysCheck())
        {
            enajeFlag = true;
            ManagerScript.enajeFlag[1-player] = true;
        }
        else if (enajeFlag)
        {
            enajeFlag = false;
            ManagerScript.EnaPhaseSkip[1 - player] = false;
        }

        //cip
        if (ManagerScript.getFieldInt(ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
        {
            BodyScript.Cost[0] = 0;
            BodyScript.Cost[1] = 3;
            BodyScript.Cost[2] = 0;
            BodyScript.Cost[3] = 0;
            BodyScript.Cost[4] = 0;
            BodyScript.Cost[5] = 0;

            if (ManagerScript.checkCost(ID, player))
                BodyScript.DialogFlag = true;
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes") && ManagerScript.checkCost(ID, player))
            {                
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(ID + player * 50);
                BodyScript.effectMotion.Add(17);
                costFlag = true;
            }
            else BodyScript.Targetable.Clear();

            BodyScript.messages.Clear();
        }
        //after cost
        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;

            int target = player;
            int f = 0;
            int num = ManagerScript.getFieldAllNum(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (ManagerScript.getCardType(x, target) == 2 && checkClass(x, target))
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.GUIcancelEnable = true;

                for (int i = 0; i < 2 && i < BodyScript.Targetable.Count; i++)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add((int)Motions.Summon);
                }

                BodyScript.effectTargetID.Add(50*target);
                BodyScript.effectMotion.Add((int)Motions.Shuffle);
            }
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            int target = player;
            int f = 0;
            int num = ManagerScript.getFieldAllNum(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x>=0)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.GUIcancelEnable = false;

                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-2);
                BodyScript.effectMotion.Add((int)Motions.GoHand);
            }

        }


 

        //update
        field = ManagerScript.getFieldInt(ID, player);
    }

    bool checkClass(int x, int cplayer)
    {
        return ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_アーム);
    }

    bool alwaysCheck()
    {
        if (ManagerScript.getFieldInt(ID, player) != (int)Fields.SIGNIZONE)
            return false;

        int target = player;
        int f = 3;
        int num = ManagerScript.getFieldAllNum(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0 && ManagerScript.getCardScr(x, target).Name == "原槍　エナジェ")
                return true;
        }

        return false;
    }
}

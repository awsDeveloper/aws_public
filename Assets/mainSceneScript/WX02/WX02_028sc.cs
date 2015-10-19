using UnityEngine;
using System.Collections;

public class WX02_028sc : MonoBehaviour
{
    GameObject Manager;
    DeckScript ManagerScript;
    GameObject Body;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;
    bool chantFlag = false;
    //	bool DialogFlag=false;
    bool askFlag = false;
    bool burstEffect = false;
    int notCipID = -1;
    bool stopFlag = false;
    int stopCount = 0;
    // Use this for initialization
    void Start()
    {
        Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //chant
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(ID + 50 * player);
            BodyScript.effectMotion.Add(22);
            BodyScript.AntiCheck = true;
        }
        //after chant
        if (ManagerScript.getFieldInt(ID, player) != 8 && field == 8 && !BodyScript.BurstFlag)
        {
            if (!BodyScript.AntiCheck)
            {
                chantFlag = true;
                askFlag = true;
            }
            else
            {
                chantFlag = false;
                BodyScript.AntiCheck = false;
            }
        }

        //chant effect
        if (chantFlag)
        {
            if (!askFlag && !BodyScript.effectFlag) askFlag = true;

            //receive
            if (BodyScript.messages.Count > 0)
            {
                askFlag = false;
                if (BodyScript.messages[0].Contains("Yes"))
                {
                    BodyScript.effectFlag = true;
                    BodyScript.effectTargetID.Add(-1);
                    BodyScript.effectMotion.Add(7);
                    int target = player;
                    int f = 0;
                    int num = ManagerScript.getFieldAllNum(f, target);
                    int x = ManagerScript.getFieldRankID(f, num - 1, target);
                    BodyScript.effectTargetID.Add(x + target * 50);
                    BodyScript.effectMotion.Add(3);
                }
                else BodyScript.Targetable.Clear();

                BodyScript.messages.Clear();
            }

            int lrigNum = ManagerScript.getFieldAllNum(4, player);
            CardScript lrig = ManagerScript.getCardScr(ManagerScript.getFieldRankID(4, lrigNum - 1, player), player);
            if (lrig.AttackNow && askFlag)
            {
                for (int i = 0; i < 3; i++)
                {
                    int x = ManagerScript.getFieldRankID(3, i, player);
                    if (x >= 0) BodyScript.Targetable.Add(x + 50 * player);
                }
                if (BodyScript.Targetable.Count > 0)
                {
                    //					ManagerScript.stopFlag=true;
                    //					DialogFlag=true;
                    BodyScript.DialogFlag = true;
                }
            }
            if (ManagerScript.getPhaseInt() == 7)
            {
                chantFlag = false;
            }
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag && ManagerScript.getFieldAllNum(3, player) < 3)
        {
            int target = player;
            int f = 7;
            int num = ManagerScript.getFieldAllNum(f, target);
            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0 && ManagerScript.getCardColor(x, target) == 5 && ManagerScript.getCardType(x, target) == 2)
                    BodyScript.Targetable.Add(x + 50 * target);
            }
            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-2);
                BodyScript.effectMotion.Add(6);
                burstEffect = true;
            }
        }
        if (burstEffect && BodyScript.effectTargetID.Count == 1 && BodyScript.effectTargetID[0] >= 0)
        {
            burstEffect = false;
            notCipID = BodyScript.effectTargetID[0];
        }
        //after 1 frame check
        if (stopFlag)
        {
            stopCount--;
            CardScript sc = ManagerScript.getCardScr(notCipID % 50, notCipID / 50);
            sc.effectFlag = false;
            sc.DialogFlag = false;
            sc.effectTargetID.Clear();
            sc.effectMotion.Clear();
            if (ManagerScript.stopFlag)
            {
                stopCount = 5;
            }
        }
        if (stopCount < 0 && stopFlag)
        {
            stopFlag = false;
            ManagerScript.cipCheck = false;
            notCipID = -1;
        }
        //stopFlag up
        if (notCipID >= 0 && !BodyScript.effectFlag && !stopFlag)
        {
            ManagerScript.cipCheck = true;
            stopFlag = true;
            stopCount = 5;
        }
        field = ManagerScript.getFieldInt(ID, player);
    }
}

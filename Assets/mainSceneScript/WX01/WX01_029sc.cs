using UnityEngine;
using System.Collections;

public class WX01_029sc : MonoBehaviour
{
    GameObject Manager;
    DeckScript ManagerScript;
    GameObject Body;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;
    bool ignition = false;
    //	bool DialogFlag=false;
 //   bool costFlag = false;

    // Use this for initialization
    void Start()
    {
        Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;
        BodyScript.powerUpValue = 2000;

        Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();


        BodyScript.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip, true, true).addEffect(cip_1);
    }

    // Update is called once per frame
    void Update()
    {
        if (ManagerScript.getFieldInt(ID, player) == 3)
        {
            int aID = ManagerScript.getAttackerID();

            if (aID != -1 && ManagerScript.getCardColor(aID % 50, aID / 50) == 2)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectMotion.Add((int)Motions.PowerUpEndPhase);
                BodyScript.effectTargetID.Add(ManagerScript.getAttackerID());
            }
        }

        /*　------------ effectTempleteで記述 ---------------------------------------------------------
                //cip
               if (ManagerScript.getFieldInt(ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
                {
                    int costNum = ManagerScript.getEnaColorNum(2, player);
                    costNum += ManagerScript.MultiEnaNum(player);
                    if (costNum >= 1)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            int x = ManagerScript.getFieldRankID(3, i, 1 - player);
                            if (x > 0 && ManagerScript.getCardPower(x, 1 - player) <= 7000)
                            {
                                BodyScript.Targetable.Add(x + 50 * (1 - player));
                            }
                        }
                        if (BodyScript.Targetable.Count > 0)
                        {
                            //					ManagerScript.stopFlag=true;
                            //					DialogFlag=true;
                            BodyScript.DialogFlag = true;
                        }
                    }
                }

                //receive
                if (BodyScript.messages.Count > 0)
                {
                    if (BodyScript.messages[0].Contains("Yes"))
                    {
                        BodyScript.effectFlag = true;
                        BodyScript.effectTargetID.Add(ID + player * 50);
                        BodyScript.effectMotion.Add(17);
                        BodyScript.Cost[0] = 0;
                        BodyScript.Cost[1] = 0;
                        BodyScript.Cost[2] = 1;
                        BodyScript.Cost[3] = 0;
                        BodyScript.Cost[4] = 0;
                        BodyScript.Cost[5] = 0;
                        costFlag = true;
                    }
                    else BodyScript.Targetable.Clear();
                    BodyScript.messages.Clear();
                }
 
        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;
            BodyScript.effectTargetID.Add(-1);
            BodyScript.effectMotion.Add(5);
        }
        --------------------------------------------------------------------------------------------
         */

        //ignition
        if (BodyScript.Ignition && !ignition)
        {
            int rc = ManagerScript.getEnaColorNum(2, player);
            int mNum = ManagerScript.MultiEnaNum(player);
            if (rc + mNum >= 2)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(ID + player * 50);
                BodyScript.effectMotion.Add(17);
                BodyScript.Cost[0] = 0;
                BodyScript.Cost[1] = 0;
                BodyScript.Cost[2] = 2;
                BodyScript.Cost[3] = 0;
                BodyScript.Cost[4] = 0;
                BodyScript.Cost[5] = 0;
                BodyScript.DoubleCrash = true;
                BodyScript.Ignition = false;
            }
        }

        if (ManagerScript.getPhaseInt() == 7)
            BodyScript.DoubleCrash = false;

        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, 1 - player);
                if (x > 0 && ManagerScript.getCardPower(x, 1 - player) <= 10000)
                {
                    BodyScript.Targetable.Add(x + 50 * (1 - player));
                }
            }
            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(5);
            }
        }

        field = ManagerScript.getFieldInt(ID, player);
        ignition = BodyScript.Ignition;
    }

    void cip()
    {
        BodyScript.setPayCost(cardColorInfo.赤, 1);
    }

    void cip_1()
    {
        BodyScript.maxPowerBanish(7000);
    }
}

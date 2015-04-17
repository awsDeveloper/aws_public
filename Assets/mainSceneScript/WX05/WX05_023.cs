using UnityEngine;
using System.Collections;

public class WX05_023 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool costFlag = false;
    int field = -1;

    bool effected = false;

    bool dontFlag = false;

    bool upFlag = false;

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
        BodyScript.Attackable = false;
        
        //always
        if (field == 3)
        {
            if (BodyScript.Targetable.Count == 0)
            {
                ManagerScript.powChanListChangerClear(ID, player);

                ManagerScript.targetableUnderCardsIn(ID, player);
                ManagerScript.alwaysChagePower(ID, player, 2000 * BodyScript.Targetable.Count, ID, player);
                BodyScript.Targetable.Clear();

                upFlag = true;
            }
        }
        else if (upFlag)
        {
            upFlag = false;
            ManagerScript.powChanListChangerClear(ID, player);
        }

        //ignition
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            if (ManagerScript.getIDConditionInt(ID, player) == 1)
            {
                costFlag = true;
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(ID + 50 * player);
                BodyScript.effectMotion.Add(8);
            }
        }

        //ignition after cost
        if (BodyScript.effectTargetID.Count == 0 && costFlag)
        {
            costFlag = false;

            ManagerScript.targetableUnderCardsIn(ID, player);

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-2);
                BodyScript.effectMotion.Add((int)Motions.GoHand);
                effected = true;
            }
        }

        //ignition after effect
        if (effected && BodyScript.effectTargetID.Count == 0)
        {
            effected = false;

            ManagerScript.targetableUnderCardsIn(ID,player);

            if (ManagerScript.getLastMotionsRear() == (int)Motions.GoHand && BodyScript.Targetable.Count == 0)
            {
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
                    BodyScript.effectMotion.Add((int)Motions.EnaCharge);
                    BodyScript.effectTargetID.Add(ID + player * 50);
                    BodyScript.effectMotion.Add((int)Motions.EnaCharge);
                    BodyScript.effectTargetID.Add(-1);
                }
            }
            else
                BodyScript.Targetable.Clear();
        }

        //cip
        if (ManagerScript.getFieldInt(ID, player) == 3 && field != 3)
        {
            int target = player;
            int f = (int)Fields.TRASH;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);

                if (x >= 0 && checkClass(x, target) && ManagerScript.getCardScr(x,target).Name !="羅原　Ｕ")
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count >= 3)
            {
                BodyScript.messages.Add("" + ManagerScript.getRank(ID, player));

                BodyScript.effectFlag = true;

                for (int i = 0; i < 3; i++)
                {
                    BodyScript.effectMotion.Add((int)Motions.GoUnderZone);
                    BodyScript.effectTargetID.Add(-2);
                }
            }
            else
            {
                BodyScript.Targetable.Clear();

                BodyScript.effectFlag = true;
                BodyScript.effectMotion.Add((int)Motions.GoTrash);
                BodyScript.effectTargetID.Add(ID+50*player);
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

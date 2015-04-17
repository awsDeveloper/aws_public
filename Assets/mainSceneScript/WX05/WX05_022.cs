using UnityEngine;
using System.Collections;

public class WX05_022 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;

    int ID = -1;
    int player = -1;
    int field = -1;
    bool costFlag = false;
    bool UpFlag = false;
    bool oneceTurn = false;

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
        //always
        if (ManagerScript.getFieldInt(ID, player) == 3)
        {
            if (!ManagerScript.CMRFlag[1 - player])
            {
                UpFlag = true;
                ManagerScript.CMRFlag[1 - player] = true;
            }
        }
        else if (UpFlag)
        {
            UpFlag = false;
            ManagerScript.CMRFlag[1 - player] = false;
        }

        //ignition
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            if (!oneceTurn)
            {
                int f = 2;
                int target = player;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);

                    if (x > 0)
                    {
                        BodyScript.effectFlag = true;
                        BodyScript.effectTargetID.Add(x + 50 * target);
                        BodyScript.effectMotion.Add(19);

                    }
                }
            }

            if (BodyScript.effectFlag)
            {
                costFlag = true;
                oneceTurn = true;
            }
        }

        //after cost
        if (BodyScript.effectTargetID.Count == 0 && costFlag)
        {
            costFlag = false;
            BodyScript.Targetable.Clear();

            if (ManagerScript.getLastMotions(0) == 19 && ManagerScript.getLastMotions(1) == 19)
            {
                int f = 3;
                int target = 1 - player;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);

                    //target check
                    if (x > 0)
                    {
                        BodyScript.Targetable.Add(x + target * 50);
                    }
                }

                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.effectTargetID.Add(-1);
                    BodyScript.effectMotion.Add(5);
                }
            }
        }

        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            int lID = ManagerScript.getLrigID(1 - player);
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(lID + 50 * (1 - player));
            BodyScript.effectMotion.Add((int)Motions.DownAndFreeze);
        }

        //update
        field = ManagerScript.getFieldInt(ID, player);
        if (ManagerScript.getPhaseInt() == (int)Phases.EnaPhase)
            oneceTurn = false;
    }
}

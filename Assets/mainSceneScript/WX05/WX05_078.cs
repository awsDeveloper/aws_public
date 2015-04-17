using UnityEngine;
using System.Collections;

public class WX05_078 : MonoBehaviour
{
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool costFlag = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.powerUpValue = -5000;
    }

    // Update is called once per frame
    void Update()
    {
        checkIgni();
        afterCost();
    }

    void checkIgni()
    {
        //ignition
        if (!BodyScript.Ignition)
            return;
        BodyScript.Ignition = false;

        if (ManagerScript.getIDConditionInt(ID, player) != 1)
            return;

        int target = player;
        int f = 3;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if (x >= 0)
            {
                int cID = ManagerScript.GetCharm(x, target);

                if (cID >= 0)
                    BodyScript.Targetable.Add(cID + 50 * target);
            }
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.effectFlag = true;
        BodyScript.effectTargetID.Add(ID + 50 * player);
        BodyScript.effectMotion.Add(8);

        BodyScript.setEffect(-1, 0, Motions.CostGoTrash);
        costFlag = true;
    }

    void afterCost()
    {
        if (!costFlag || BodyScript.effectTargetID.Count>0)
            return;

        costFlag = false;

        int target = 1 - player;
        int f = 3;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if (x >= 0)
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.setEffect(-1, 0, Motions.PowerUpEndPhase);
    }

}

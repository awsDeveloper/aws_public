using UnityEngine;
using System.Collections;

public class WX05_045 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool bursted = false;
    bool cFlag = false;

    bool chantedFlag = false;
    int count = 0;
    int effectPhase = 0;

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
        if (ManagerScript.getStartedPhase() == (int)Phases.UpPhase)
            chantedFlag = false;

        //chant
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            BodyScript.setEffect(0, player, Motions.AntiCheck);
            BodyScript.AntiCheck = true;
            cFlag = true;
        }

        //anti check
        if (cFlag && BodyScript.effectTargetID.Count == 0)
        {
            cFlag = false;
            chantedFlag = true;

            BodyScript.setEffect(ManagerScript.getLrigID(player), player, Motions.AddIgniEnd);
            BodyScript.IgniAddID = ID + player * 50;
        }

        //end all hand death
        if (chantedFlag && ManagerScript.getStartedPhase() == (int)Phases.EndPhase)
        {
            chantedFlag = false;

            allHandDeath();
        }

        //ignition
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            if (ManagerScript.getIDConditionInt(ID, player) == 1)
            {
                BodyScript.setEffect(ID, player, Motions.Down);
                allBanish(1 - player);
             }
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            BodyScript.DialogFlag = true;
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0] == "Yes")
            {
                allHandDeath();
                allBanish(1 - player);
                allBanish(player);
            }

            BodyScript.messages.Clear();
        }

        //UpDate
        field = ManagerScript.getFieldInt(ID, player);
    }

    void allBanish(int target)
    {
        int f = 3;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0)
                BodyScript.setEffect(x, target, Motions.EnaCharge);
        }
    }

    void allHandDeath()
    {
        int target = player;
        int f = (int)Fields.HAND;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0)
                BodyScript.setEffect(x, target, Motions.GoTrash);
        }
    }
}

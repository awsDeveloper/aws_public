using UnityEngine;
using System.Collections;

public class WX06_021 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

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
        triggered();

        ignition();

        burst();
    }

    void triggered()
    {
        int exID = ManagerScript.getExitID( -1,(int)Fields.SIGNIZONE);

        if (!BodyScript.isOnBattleField()
            || !ManagerScript.checkClass(exID, cardClassInfo.精武_毒牙)
            || exID / 50 != player
            || ManagerScript.getIDConditionInt(ID, player) != (int)Conditions.Down)
            return;

        BodyScript.setEffect(ID, player, Motions.Up);
    }

    void ignition()
    {
        if (!BodyScript.Ignition)
            return;

        BodyScript.Ignition = false;

        if (ManagerScript.getIDConditionInt(ID, player) != (int)Conditions.Up)
            return;

        int target = 1-player;
        int f = (int)Fields.SIGNIZONE;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0)
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.setEffect(ID, player, Motions.Down);

        BodyScript.powerUpValue = -3000;
        BodyScript.setEffect(-1, 0, Motions.PowerUpEndPhase);
    }

    void burst()
    {
        if (!BodyScript.isBursted())
            return;

        int target = 1 - player;
        int f = (int)Fields.SIGNIZONE;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0)
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.powerUpValue = -3000;
        BodyScript.setEffect(-1, 0, Motions.PowerUpLevelEnd);
    }
}

using UnityEngine;
using System.Collections;

public class WX06_017 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;


    bool afterBustFlag = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        if (gameObject.GetComponent<ClassCountPowerUp>() == null)
            gameObject.AddComponent<ClassCountPowerUp>();
        ClassCountPowerUp cpu = gameObject.GetComponent<ClassCountPowerUp>();
        cpu.setClassList(cardClassInfo.精武_アーム);
    }

    // Update is called once per frame
    void Update()
    {
        cip();

        banished();

        received();

        burst();
    }

    void banished()
    {
        if (!ManagerScript.checkClass(ManagerScript.getBanishedID(), cardClassInfo.精武_アーム) || ManagerScript.getBanishedID() / 50 != player)
            return;

        if (ManagerScript.getBanishedID() != ID + 50 * player && !BodyScript.isOnBattleField())
            return;

        BodyScript.DialogFlag = true;
        BodyScript.DialogNum = 5;
    }

    void received()
    {
        if (BodyScript.messages.Count == 0)
            return;

        string str = BodyScript.messages[0];
        BodyScript.messages.Clear();

        if (BodyScript.DialogNum == 5)
        {
            int count = int.Parse(str);

            Motions m = Motions.Up;
            if (count == 1)
                m = Motions.EffectDown;

            int target = player;
            int f = (int)Fields.SIGNIZONE;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            target = 1 - target;
            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count == 0)
                return;

            BodyScript.setEffect(-1, 0, m);
        }
        else if (BodyScript.DialogNum == 0 && str=="Yes")
        {
            BodyScript.changeColorCost((int)cardColorInfo.白, 1);
            if (!ManagerScript.checkCost(ID, player))
                return;

            BodyScript.setEffect(ID, player, Motions.PayCost);

            int target = player;
            int f = (int)Fields.MAINDECK;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (ManagerScript.checkContainsName(x,target,"拳"))
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count == 0)
                return;

            BodyScript.setEffect(-2, 0, Motions.GoHand);
        }
    }

    void cip()
    {
        if (!BodyScript.isCiped())
            return;

        BodyScript.DialogFlag = true;
        BodyScript.DialogNum = 0;
    }

    void burst()
    {
        if (!BodyScript.isBursted())
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

        BodyScript.setEffect(-1, 0, Motions.GoHand);
    }
}
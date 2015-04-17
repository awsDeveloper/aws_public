using UnityEngine;
using System.Collections;

public class WX06_016 : MonoBehaviour
{
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
        cpu.setClassList(cardClassInfo.精像_天使);
    }

    // Update is called once per frame
    void Update()
    {

        removed();

        burst();

        afterBurst();
    }

    void removed()
    {
        if (!BodyScript.isYourEffRemoved())
            return;

        int target = player;
        int f = (int)Fields.MAINDECK;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (ManagerScript.checkClass(x, target, cardClassInfo.精像_天使) && ManagerScript.getCardLevel(x, target) <= 3)
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.setEffect(-2, 0, Motions.Summon);
    }

    void burst()
    {
        if (!BodyScript.isBursted())
            return;

        int target = player;
        int f = (int)Fields.MAINDECK;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (ManagerScript.checkClass(x, target, cardClassInfo.精像_天使))
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.setEffect(-2, 0, Motions.GoHand);
        afterBustFlag = true;
        BodyScript.TargetIDEnable = true;
    }

    void afterBurst()
    {
        if (!afterBustFlag || BodyScript.effectTargetID.Count > 0)
            return;

        afterBustFlag = false;
        BodyScript.Targetable.Clear();

        int tID = BodyScript.TargetID[0];
        BodyScript.TargetID.Clear();
        BodyScript.TargetIDEnable = false;

        int tLev = ManagerScript.getCardLevel(tID % 50, tID / 50);

        int target = player;
        int f = (int)Fields.MAINDECK;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (ManagerScript.checkClass(x, target, cardClassInfo.精像_天使) && ManagerScript.getCardLevel(x, target) == tLev)
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.setEffect(-2, 0, Motions.GoHand);
    }
}

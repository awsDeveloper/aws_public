using UnityEngine;
using System.Collections;

public class WX06_018 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool afterAttackFlag = false;

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
        attack();

        afterAttack();

        burst();
    }

    void attack()
    {
        if (ManagerScript.getAttackerID() != ID + 50 * player)
            return;

        BodyScript.setEffect(0, player, Motions.TopGoTrash);
        BodyScript.setEffect(0, player, Motions.TopGoTrash);
        BodyScript.setEffect(0, player, Motions.TopGoTrash);

        afterAttackFlag = true;
    }

    void afterAttack()
    {
        if (!afterAttackFlag || BodyScript.effectTargetID.Count > 0)
            return;

        afterAttackFlag = false;

        int index = 0;
        int count = 0;

        while (true)
        {
            int x = ManagerScript.getTopGoTrashListID(index, ID, player);

            if (x == -1)
                break;

            if (ManagerScript.checkClass(x, cardClassInfo.精武_ウェポン))
                count++;

            index++;
        }

        int line = 0;

        if (count == 1)
            line = 3000;
        else if (count == 2)
            line = 8000;
        else if (count == 3)
            line = 15000;

        int target = 1 - player;
        int f = (int)Fields.SIGNIZONE;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x>=0 && ManagerScript.getCardPower(x, target) <= line)
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.setEffect(-1, 0, Motions.EnaCharge);
    }

    void burst()
    {
        if (!BodyScript.isBursted())
            return;

        int target = 1 - player;
        int f = (int)Fields.SIGNIZONE;
        int num = ManagerScript.getNumForCard(f, target);

        int t=ManagerScript.getClassNum(player, Fields.TRASH, cardClassInfo.精武_ウェポン);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x>=0 && ManagerScript.getCardPower(x, target) <= 3000 * t)
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.setEffect(-1, 0, Motions.EnaCharge);
    }
}

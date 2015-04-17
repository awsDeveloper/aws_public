using UnityEngine;
using System.Collections;

public class WX06_001 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool costFlag_1 = false;
    bool costFlag_2 = false;

    int exCost = 7;

    bool onceTurn_1 = false;
    bool onceTurn_2 = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        //dialog
        BodyScript.checkStr.Add("バニッシュ");
        BodyScript.checkStr.Add("トラッシュ送り");

        for (int i = 0; i < BodyScript.checkStr.Count; i++)
            BodyScript.checkBox.Add(false);

        if(gameObject.GetComponent<classPowerUp>()==null)
            gameObject.AddComponent<classPowerUp>();
        classPowerUp cpu = gameObject.GetComponent<classPowerUp>();
        cpu.setClassList(cardClassInfo.精像_天使);
        cpu.puv = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        //ignition
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            targetIn(true);
            int count = BodyScript.Targetable.Count;

            ManagerScript.targetableSameNameRemove(BodyScript);
            
            int count_2 = BodyScript.Targetable.Count;
            BodyScript.Targetable.Clear();

            bool flag_1 = count >= exCost && !onceTurn_1;
            bool flag_2 = count_2 >= exCost && !onceTurn_2;

            if (flag_1 && flag_2)
            {
                BodyScript.Targetable.Clear();

                BodyScript.DialogFlag = true;
                BodyScript.DialogNum = 2;
                BodyScript.DialogCountMax = 1;
            }
            else if (flag_2)
                effect_2();
            else if (flag_1)
                effect_1();

        }

        //ignition after cost
        if (costFlag_1 && BodyScript.effectTargetID.Count == 0)
        {
            costFlag_1 = false;
            onceTurn_1 = true;

            if (ManagerScript.getLastMotionsRear() == (int)Motions.GoDeck)
            {
                BodyScript.Targetable.Clear();

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
                    BodyScript.setEffect(-1, 0, Motions.EnaCharge);

                BodyScript.setEffect(0, player, Motions.Shuffle);
            }
        }

        //ignition after cost_2
        if (costFlag_2 && BodyScript.effectTargetID.Count == 0)
        {
            costFlag_2 = false;
            onceTurn_2 = true;
            BodyScript.Targetable.Clear();

            if (ManagerScript.getLastMotionsRear() == (int)Motions.GoDeck)
            {
                BodyScript.Targetable.Clear();

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
                    BodyScript.setEffect(-1, 0, Motions.GoTrash);

                BodyScript.setEffect(0, player, Motions.Shuffle);
            }
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                if (BodyScript.checkBox[0]) effect_1();
                else if (BodyScript.checkBox[1]) effect_2();
            }

            BodyScript.messages.Clear();
        }

        if (ManagerScript.getStartedPhase() == (int)Phases.UpPhase)
        {
            onceTurn_1 = false;
            onceTurn_2 = false;
        }

    }

    void targetIn(bool isMode_1)
    {
        int target = player;
        int f = (int)Fields.TRASH;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if (ManagerScript.checkClass(x,target, cardClassInfo.精像_天使))
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (!isMode_1)
            ManagerScript.targetableSameNameRemove(BodyScript);
    }

    void effect_1()
    {
        costFlag_1 = true;

        targetIn(true);
        BodyScript.effectFlag = true;

        for (int i = 0; i < exCost; i++)
        {
            BodyScript.effectTargetID.Add(-2);
            BodyScript.effectMotion.Add((int)Motions.GoDeck);
        }
    }

    void effect_2()
    {
        costFlag_2 = true;

        targetIn(false);
        BodyScript.effectFlag = true;

        for (int i = 0; i < exCost; i++)
        {
            BodyScript.effectTargetID.Add(-2);
            BodyScript.effectMotion.Add((int)Motions.GoDeck);
        }
    }
}

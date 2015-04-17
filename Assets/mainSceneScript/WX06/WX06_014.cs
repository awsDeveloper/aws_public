using UnityEngine;
using System.Collections;

public class WX06_014 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool costFlag_1 = false;

    int exCost = 1;
    bool exFlag = false;
    bool onceTurn = false;

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
        //useLim
        int nowLrigID = ManagerScript.getLrigID(player);
        if (nowLrigID >= 0)
            BodyScript.useLimit = !ManagerScript.getCardScr(nowLrigID, player).Name.Contains("ウムル");

        //cip
        if (ManagerScript.getCipID() == ID + 50 * player)
        {
            BodyScript.DialogFlag = true;
            BodyScript.DialogNum = 1;
            BodyScript.DialogCountMax = ManagerScript.getFieldAllNum((int)Fields.MAINDECK, player);
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            int count = int.Parse(BodyScript.messages[0]);

            for (int i = 0; i < count; i++)
                BodyScript.setEffect(0, player, Motions.TopGoTrash);

            BodyScript.messages.Clear();
        }

        //ignition
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            if (!onceTurn)
            {
                ManagerScript.targetableExceedIn(player, BodyScript);

                if (BodyScript.Targetable.Count >= exCost)
                {
                    BodyScript.effectFlag = true;

                    exFlag = true;
                    onceTurn = true;
                    for (int i = 0; i < exCost; i++)
                    {
                        BodyScript.effectTargetID.Add(-2);
                        BodyScript.effectMotion.Add((int)Motions.Exceed);
                    }
                }
                else
                    BodyScript.Targetable.Clear();
            }
        }

        //ignition after cost
        if (exFlag && BodyScript.effectTargetID.Count == 0)
        {
            exFlag = false;
            BodyScript.Targetable.Clear();

            if (ManagerScript.getLastMotionsRear() == (int)Motions.Exceed)
            {
                int target = player;
                int f = (int)Fields.TRASH;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);
                    if (ManagerScript.checkClass(x, target, cardClassInfo.精械_古代兵器))
                        BodyScript.Targetable.Add(x + target * 50);
                }

                if (BodyScript.Targetable.Count >= 5)
                {
                    for (int i = 0; i < 5; i++)
                        BodyScript.setEffect(-2, 0, Motions.GoDeckBottom);
                    costFlag_1 = true;
                }
                else
                    BodyScript.Targetable.Clear();
            }
        }

        //after cost_1
        if (costFlag_1 && BodyScript.effectTargetID.Count == 0)
        {
            costFlag_1 = false;
            BodyScript.Targetable.Clear();

            int target = 1 - player;
            int f = (int)Fields.SIGNIZONE;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0)
                    BodyScript.Targetable.Add(x + target * 50);
            }

            if (BodyScript.Targetable.Count > 0)
                BodyScript.setEffect(-1, 0, Motions.EnaCharge);
        }

        if (ManagerScript.getStartedPhase() == (int)Phases.UpPhase)
            onceTurn = false;
    }
}

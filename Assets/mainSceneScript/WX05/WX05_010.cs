using UnityEngine;
using System.Collections;

public class WX05_010 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool costFlag_1 = false;
    bool costFlag_2 = false;

    int exCost = 2;
    bool exFlag = false;

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
            BodyScript.useLimit = !ManagerScript.getCardScr(nowLrigID, player).Name.Contains("エルドラ");

        //cip
        if (ManagerScript.getCipID() == ID + 50 * player)
        {
            int target = player;
            int f = (int)Fields.LIFECLOTH;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.cursorCancel = true;

                BodyScript.effectFlag = true;

                for (int i = 0; i < BodyScript.Targetable.Count; i++)
                {
                    BodyScript.effectMotion.Add((int)Motions.GoTrash);
                    BodyScript.effectTargetID.Add(-1);
                }

                BodyScript.effectTargetID.Add(50 * target);
                BodyScript.effectMotion.Add((int)Motions.LifeClothSort);

                costFlag_1 = true;
            }
        }
        //after cost_1
        if (costFlag_1 && BodyScript.inputReturn > 0)
        {
            costFlag_1 = false;
            costFlag_2 = true;

            for (int i = 0; i < BodyScript.inputReturn; i++)
            {
                BodyScript.effectTargetID.Add(50 * player);
                BodyScript.effectMotion.Add((int)Motions.TopGoLifeCloth);
            }

            BodyScript.inputReturn = -1;
        }
        //after cost_2
        if (costFlag_2 && BodyScript.effectTargetID.Count == 0)
        {
            costFlag_2 = false;
            for (int i = 0; i < ManagerScript.getFieldAllNum((int)Fields.LIFECLOTH, player); i++)
            {
                int x = ManagerScript.getFieldRankID((int)Fields.LIFECLOTH, i, player);
                BodyScript.effectTargetID.Add(x + 50 * player);
                BodyScript.effectMotion.Add((int)Motions.checkBackLifeCloth);
            }
        }

        //ignition
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            ManagerScript.targetableExceedIn(player, BodyScript);

            if (BodyScript.Targetable.Count >= exCost)
            {
                BodyScript.effectFlag = true;

                exFlag = true;
                for (int i = 0; i < exCost; i++)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add((int)Motions.Exceed);
                }
            }
            else
                BodyScript.Targetable.Clear();
        }

        //ignition after cost
        if (exFlag && BodyScript.effectTargetID.Count == 0)
        {
            exFlag = false;

            if (ManagerScript.getLastMotionsRear() == (int)Motions.Exceed)
            {
                int target = player;
                BodyScript.effectFlag = true;

                BodyScript.effectMotion.Add((int)Motions.TopCrash);
                BodyScript.effectTargetID.Add(50 * target);

                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add((int)Motions.LifeSetFromHand);
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class WX06_027 : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        //cip
        if (BodyScript.isCiped() && ManagerScript.getFieldAllNum((int)Fields.ENAZONE, 1 - player) <= 4)
        {
            targetIN();

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.Targetable.Clear();
                BodyScript.DialogFlag = true;
            }
        }

        //after cost
        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;

            int target = 1-player;
            int f = (int)Fields.SIGNIZONE;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0 && ManagerScript.getCardPower(x, target) <= 8000)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
                BodyScript.setEffect(-1, 0, Motions.EnaCharge);
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {

                targetIN();

                if (BodyScript.Targetable.Count > 0)
                {
                    costFlag = true;
                    BodyScript.effectFlag = true;
                    BodyScript.effectTargetID.Add(-1);
                    BodyScript.effectMotion.Add((int)Motions.HandDeath);
                }
            }

            BodyScript.messages.Clear();
        }


        //burst
        if (BodyScript.isBursted())
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add((int)Motions.TopEnaCharge);

        }
    }

    void targetIN()
    {
        int target = player;
        int f = 2;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0 && ManagerScript.checkClass(x, target, cardClassInfo.精生_龍獣))
                BodyScript.Targetable.Add(x + 50 * target);
        }
    }
}

using UnityEngine;
using System.Collections;

public class WX05_068 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool costFlag = false;

    bool afterEffect = false;

    int field = -1;

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
        if (ManagerScript.getFieldInt(ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
        {
            targetIN();

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.Targetable.Clear();
                BodyScript.DialogFlag = true;
            }
         }

        //after cost
        if (costFlag && BodyScript.effectTargetID.Count == 0 )
        {
            costFlag = false;
            afterEffect = true;

            BodyScript.setEffect(0, player, Motions.TopGoShowZone);
            BodyScript.setEffect(0, player, Motions.TopGoShowZone);
        }

        //after effect
        if (afterEffect && BodyScript.effectTargetID.Count == 0)
        {
            afterEffect = false;

            int index=0;
            while(true)
            {
                int x = ManagerScript.getShowZoneID(index);

                if (x < 0)
                    break;

                if (ManagerScript.checkClass(x % 50, x / 50, cardClassInfo.精像_美巧))
                    BodyScript.setEffect(x % 50, x / 50, Motions.GoHand);
                else
                    BodyScript.Targetable.Add(x);
                index++;
            }

            if (BodyScript.Targetable.Count > 0)
            {
                for (int i = 0; i < BodyScript.Targetable.Count; i++)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add((int)Motions.GoDeckBottom);
                }
            }
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
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50*player);
            BodyScript.effectMotion.Add((int)Motions.TopEnaCharge);

        }

        //update
        field = ManagerScript.getFieldInt(ID, player);
    }

    void targetIN()
    {
        int target = player;
        int f = 2;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0 && ManagerScript.checkClass(x, target, cardClassInfo.精像_美巧))
                BodyScript.Targetable.Add(x + 50 * target);
        }
    }
}

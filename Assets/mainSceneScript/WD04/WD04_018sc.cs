using UnityEngine;
using System.Collections;

public class WD04_018sc : MonoBehaviour
{
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
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
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, player);
                if (x >= 0 && ManagerScript.getSigniConditionInt(i, player) == 1)
                {
                    BodyScript.Targetable.Add(x + 50 * player);
                }
            }
            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.TargetIDEnable = true;
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add((int)Motions.EffectDown);
            }
        }

        if (BodyScript.TargetID.Count == 1)
        {
            BodyScript.Targetable.Clear();

            int tID = BodyScript.TargetID[0];
            int p = ManagerScript.getCardPower(tID % 50, tID / 50);
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, 1 - player);
                if (x >= 0 && p >= ManagerScript.getCardPower(x, 1 - player))
                {
                    BodyScript.Targetable.Add(x + 50 * (1 - player));
                }
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add(5);
            }

            BodyScript.TargetIDEnable = false;
            BodyScript.TargetID.Clear();
        }

        field = ManagerScript.getFieldInt(ID, player);
    }
}

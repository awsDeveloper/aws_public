using UnityEngine;
using System.Collections;

public class WX05_040 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool bursted = false;

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
        //triggered
        if (field == 3 && (torigger1() || torigger2()))
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add((int)Motions.TopEnaCharge);
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add((int)Motions.TopEnaCharge);

            bursted = true;
        }

        //after burst
        if (bursted && BodyScript.effectTargetID.Count==0)
        {
            bursted = false;

            int target = player;
            int f = (int)Fields.ENAZONE;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);

                if (x >= 0)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.setEffect(-1, 0, Motions.GoHand);
                BodyScript.setEffect(0, target, Motions.EnaSort);
            }
        }

        //UpDate
        field = ManagerScript.getFieldInt(ID, player);
    }

    bool torigger1()
    {
        if (ManagerScript.getPhaseInt() != (int)Phases.MainPhase)
            return false;

        int x = ManagerScript.getBanishedID();

        if (x < 0)
            return false;

        return x / 50 == 1 - player;
    }

    bool torigger2()
    {
        int x = ManagerScript.EffectDownedID;

        return x >= 0 && x / 50 == player;
    }
}

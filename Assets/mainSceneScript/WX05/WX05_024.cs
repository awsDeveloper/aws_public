using UnityEngine;
using System.Collections;

public class WX05_024 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;

    int ID = -1;
    int player = -1;
    int field = -1;
    bool UpFlag = false;

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
        //always
        if (ManagerScript.getFieldInt(ID, player) == 3)
        {
            if (!ManagerScript.Rian_pow_Flag[player] || ManagerScript.Rian_eff_Flag[player])
            {
                UpFlag = true;
                ManagerScript.Rian_pow_Flag[player] = true;
                ManagerScript.Rian_eff_Flag[player] = true;

                ManagerScript.powChanListPlayerClear(1 - player);
            }
        }
        else if (UpFlag)
        {
            UpFlag = false;
            ManagerScript.Rian_pow_Flag[player] = false;
            ManagerScript.Rian_eff_Flag[player] = false;
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add( 50 * player);
            BodyScript.effectMotion.Add((int)Motions.TopEnaCharge);

            int f = 3;
            int target = 1-player;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);

                if (x >= 0 && ManagerScript.getCardPower(x, target) >= 10000)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectMotion.Add(5);
                BodyScript.effectTargetID.Add(-1);
            }

        }

        //update
        field = ManagerScript.getFieldInt(ID, player);
    }
}

using UnityEngine;
using System.Collections;

public class WX03_031 : MonoBehaviour {
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
        BodyScript.powerUpValue = 5000;
        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();
    }
    // Update is called once per frame
    void Update()
    {
        //cip
        if (ManagerScript.getFieldInt(ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
        {
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, player);
                if (x >= 0)
                {
                    BodyScript.effectFlag = true;
                    BodyScript.effectTargetID.Add(x + 50 * player);
                    BodyScript.effectMotion.Add(34);
                }
            }
        }

        //triggered
        if (ManagerScript.getFieldInt(ID, player) == 3 && ManagerScript.CrashedID != -1)
        {
            int cID = ManagerScript.CrasherID;

            if (cID >= 0 && cID / 50 == player && ManagerScript.getCardType(cID%50,cID/50)==2)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectMotion.Add((int)Motions.TopEnaCharge);
                BodyScript.effectTargetID.Add(50 * player);
            }
        }

        field = ManagerScript.getFieldInt(ID, player);

    }
}

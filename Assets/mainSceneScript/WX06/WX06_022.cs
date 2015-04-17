using UnityEngine;
using System.Collections;

public class WX06_022 : MonoBehaviour
{
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool baseFlag = false;

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
        if (BodyScript.isCenter() && ManagerScript.checkLrigColor(player, cardColorInfo.白))
        {
            baseFlag = true;
            ManagerScript.changeBasePower(ID, player, 10000);
            BodyScript.ResiYourEffBanish = true;
        }
        else if (baseFlag)
        {
            baseFlag = false;
            ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower);
            BodyScript.ResiYourEffBanish = false;
        }
    }
}

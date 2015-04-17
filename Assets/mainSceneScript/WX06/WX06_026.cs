using UnityEngine;
using System.Collections;

public class WX06_026 : MonoBehaviour {
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
        if (BodyScript.isCenter() && ManagerScript.checkLrigColor(player, cardColorInfo.赤))
        {
            baseFlag = true;
            BodyScript.DoubleCrash = true;
        }
        else if (baseFlag)
        {
            baseFlag = false;
            BodyScript.DoubleCrash = false;
        }
    }
}

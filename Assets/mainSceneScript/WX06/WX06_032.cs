using UnityEngine;
using System.Collections;

public class WX06_032 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

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
        if (BodyScript.isCenter() && ManagerScript.checkLrigColor(player, cardColorInfo.緑) && ManagerScript.getAttackerID() == ID + 50 * player)
        {
            BodyScript.setEffect(0, player, Motions.TopEnaCharge);
        }
    }
}

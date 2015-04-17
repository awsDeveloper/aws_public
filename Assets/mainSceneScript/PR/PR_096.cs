using UnityEngine;
using System.Collections;

public class PR_096 : MonoBehaviour {
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

        BodyScript.attackArts = true;
    }

    // Update is called once per frame
    void Update()
    {
        chant();
    }

    void chant()
    {
        if (!BodyScript.isChanted() || ManagerScript.getTurnPlayer()==player)
            return;

        BodyScript.powerUpValue = 5000;

        BodyScript.setEffect(0, player, Motions.PowerUpAllEnd);
    }
}

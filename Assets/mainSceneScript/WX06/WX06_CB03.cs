using UnityEngine;
using System.Collections;

public class WX06_CB03 : MonoBehaviour {
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
        if (BodyScript.isOnBattleField() && ManagerScript.getStartedPhase() == (int)Phases.EndPhase && ManagerScript.getTurnPlayer() == 1 - player)
            BodyScript.DialogFlag = true;

        receive();

        afterCost();

        burst();
    }

    void receive()
    {
        //receive
        if (BodyScript.messages.Count == 0)
            return;

        if (BodyScript.messages[0].Contains("Yes"))
        {
            costFlag = true;
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(ID + 50 * player);
            BodyScript.effectMotion.Add((int)Motions.GoTrash);
        }

        BodyScript.messages.Clear();
    }

    void afterCost()
    {
        if (!costFlag || BodyScript.effectTargetID.Count > 0)
            return;

        costFlag = false;


        BodyScript.setEffect(0, player, Motions.TopEnaCharge);
        BodyScript.setEffect(0, player, Motions.TopEnaCharge);
    }

    void burst()
    {
        if (!BodyScript.isBursted())
            return;

        BodyScript.setEffect(0, player, Motions.TopEnaCharge);
    }
}
﻿using UnityEngine;
using System.Collections;

public class WX06_035 : MonoBehaviour {
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
        if (BodyScript.isCenter() && ManagerScript.checkLrigColor(player, cardColorInfo.黒) && ManagerScript.getAttackerID() == ID + 50 * player)
        {
            int target = 1 - player;
            int f = (int)Fields.SIGNIZONE;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count == 0)
                return;

            BodyScript.powerUpValue = -3000;
            BodyScript.setEffect(-1, 0, Motions.PowerUpEndPhase);
        }
    }
}

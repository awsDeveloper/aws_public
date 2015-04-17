using UnityEngine;
using System.Collections;

public class WX05_066 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool effectFlag = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.checkStr.Add("ハンデス");
        BodyScript.checkStr.Add("手札交換");

        for (int i = 0; i < BodyScript.checkStr.Count; i++)
        {
            BodyScript.checkBox.Add(false);

        }

    }
    // Update is called once per frame
    void Update()
    {
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            BodyScript.effectSelecter = player; 

            BodyScript.DialogFlag = true;
            BodyScript.DialogCountMax = 2;
            BodyScript.DialogNum = 2;
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                effect_1();
                effect_2();
            }

            BodyScript.messages.Clear();
        }

        if (effectFlag && BodyScript.effectTargetID.Count == 0)
        {
            effectFlag = false;

            BodyScript.effectSelecter = player;

            BodyScript.setEffect(0, player, Motions.Draw);
            BodyScript.setEffect(0, player, Motions.Draw);
            BodyScript.setEffect(0, player, Motions.Draw);

            BodyScript.setEffect(0, player, Motions.oneHandDeath);
        }

        field = ManagerScript.getFieldInt(ID, player);
    }

    void effect_1()
    {
        if (!BodyScript.checkBox[0])
            return;

        BodyScript.effectSelecter = 1 - player;
        BodyScript.setEffect(0, 1-player, Motions.oneHandDeath);
     }

    void effect_2()
    {
        if (!BodyScript.checkBox[1])
            return;

        if (ManagerScript.getLrigColor(player) != 3 || ManagerScript.getLrigLevel(player) < 4)
            return;

        effectFlag = true;
    }
}

using UnityEngine;
using System.Collections;

public class WX06_031 : MonoBehaviour {
    DeckScript ms;
    CardScript bs;
    int ID = -1;
    int player = -1;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        bs = Body.GetComponent<CardScript>();
        ID = bs.ID;
        player = bs.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ms = Manager.GetComponent<DeckScript>();

        bs.checkStr.Add("ドロー");
        bs.checkStr.Add("ハンデス");

        for (int i = 0; i < bs.checkStr.Count; i++)
            bs.checkBox.Add(false);

        bs.attackArts = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (bs.isChanted())
        {
            bs.DialogFlag = true;
            bs.DialogCountMax = 1;
            bs.DialogNum = 2;
        }

        //receive
        if (bs.messages.Count > 0)
        {
            if (bs.messages[0].Contains("Yes"))
            {
                effect_1();
                effect_2();
            }

            bs.messages.Clear();
        }
    }

    void effect_1()
    {
        if (!bs.checkBox[0])
            return;

        bs.setEffect(0, player, Motions.Draw);
        bs.setEffect(0, player, Motions.Draw);
    }

    void effect_2()
    {
        if (!bs.checkBox[1])
            return;

        bs.effectSelecter = 1 - player;
        bs.setEffect(0, 1 - player, Motions.oneHandDeath);
        bs.setEffect(0, 1 - player, Motions.oneHandDeath);
    }
}
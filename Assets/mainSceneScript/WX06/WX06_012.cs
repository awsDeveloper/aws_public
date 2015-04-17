using UnityEngine;
using System.Collections;

public class WX06_012 : MonoBehaviour {
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
    }
    // Update is called once per frame
    void Update()
    {
        cip();
    }

    void cip()
    {
        if (ms.getCipID() != ID + 50 * player)
            return;

        bs.addParameta(parametaKey.CostDownColor, (int)cardColorInfo.青);
        bs.addParameta(parametaKey.CostDownNum, 1);
        bs.addParameta(parametaKey.SpellOrArts, 0);

        bs.setEffect(0, player, Motions.CostDown);
    }
}

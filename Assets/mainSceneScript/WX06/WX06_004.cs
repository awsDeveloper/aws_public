using UnityEngine;
using System.Collections;

public class WX06_004 : MonoBehaviour {
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

        bs.attackArts = true;
        bs.notMainArts = true;
    }
    // Update is called once per frame
    void Update()
    {
        chanted();
    }

    void chanted()
    {
        if (!bs.isChanted())
            return;

        int target = 1 - player;
        int f = 3;
        int num = ms.getNumForCard(f, target);

        for (int i = 0; i < 3; i++)
        {
            int x = ms.getFieldRankID(3, i, target);
            if (x > 0)
                bs.Targetable.Add(x + 50 * target);
        }

        if (bs.Targetable.Count > 0)
            bs.setEffect(-1, 0, Motions.EffectDown);

        if (bs.Targetable.Count >= 2 && check())
            bs.setEffect(-1, 0, Motions.EffectDown);
    }

    bool check()
    {
        return ms.checkLrigColor(player, cardColorInfo.青) && ms.getFieldAllNum((int)Fields.LIFECLOTH, player) <= 2;
    }
}

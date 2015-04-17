using UnityEngine;
using System.Collections;

public class WX06_015 : MonoBehaviour {
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
        chanted();
    }

    void chanted()
    {
        if (!bs.isChanted())
            return;

        int target = player;
        int f = (int)Fields.TRASH;
        int num = ms.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ms.getFieldRankID(f, i, target);
            if(ms.checkLrigSameColor(x,target) && ms.checkType(x,target, cardTypeInfo.スペル))
                bs.Targetable.Add(x + 50 * target);
        }

        if (bs.Targetable.Count > 0)
            bs.setEffect(-2, 0, Motions.GoHand);
    }
}

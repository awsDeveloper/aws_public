using UnityEngine;
using System.Collections;

public class WX06_033 : MonoBehaviour {
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
        cip();

        burst();
    }

    void cip()
    {
        //cip
        if (!BodyScript.isCiped())
            return;

        BodyScript.setEffect(0, player, Motions.GridCountUp);
    }

    void burst()
    {
        //burst
        if (!BodyScript.isBursted())
            return;

        int target = player;
        int f = (int)Fields.MAINDECK;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0 && ManagerScript.checkClass(x,target, cardClassInfo.精像_美巧))
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.setEffect(-2, 0, Motions.GoHand);

    }
}
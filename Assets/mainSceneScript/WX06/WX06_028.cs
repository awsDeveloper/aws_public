using UnityEngine;
using System.Collections;

public class WX06_028 : MonoBehaviour {
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
        BodyScript.useLimit = !BodyScript.isFieldNumMany(Fields.LIFECLOTH, 1 - player);

        chant();

        afterInput();

        burst();
    }

    void chant()
    {
        if (!BodyScript.isChanted())
            return;

        banish(12000,-1);
        BodyScript.TargetIDEnable = true;
        BodyScript.BeforeCutInNum = 2;
    }

    void banish(int max,int tID)
    {
        int target = 1 - player;
        int f = (int)Fields.SIGNIZONE;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0 && x != tID && ManagerScript.getCardPower(x, target) <= max)
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.setEffect(-1, 0, Motions.EnaCharge);
    }

    void afterInput()
    {
        int tID =BodyScript.getTargetID();
        if (tID < 0)
            return;
        BodyScript.Targetable.Clear();
        BodyScript.TargetIDEnable = false;

        banish(10000,tID);
    }

    void burst()
    {
        //burst
        if (!BodyScript.isBursted())
            return;

        BodyScript.setEffect(0, player, Motions.Draw);
        BodyScript.setEffect(0, player, Motions.Draw);
        BodyScript.setEffect(0, player, Motions.TopCrash);
    }
}

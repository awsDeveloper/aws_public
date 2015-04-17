using UnityEngine;
using System.Collections;

public class PR_113 : MonoBehaviour {
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
        chant();

        afterChant();
    }

    void chant()
    {
        if (!BodyScript.isChanted())
            return;

        BodyScript.setEffect(-1, 0, Motions.CostBanish);
    }

    void afterChant()
    {
        if (!BodyScript.costBanish)
            return;

        BodyScript.costBanish = false;
        BodyScript.Targetable.Clear();

        BodyScript.funcTargetIn(1 - player, Fields.SIGNIZONE, check);
        BodyScript.setEffect(-1, 0, Motions.EnaCharge);
    }

    bool check(int x, int target)
    {
        return ManagerScript.getCardPower(x, target) >= 12000;
    }
}

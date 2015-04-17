using UnityEngine;
using System.Collections;

public class PR_141 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool afterIgni = false;

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
	void Update () {
        igni();

        afterIgnition();

        burst();
 	}

    void burst()
    {
        if (!BodyScript.isBursted())
            return;

        BodyScript.setEffect(0, player, Motions.Draw);
    }

    void igni()
    {
        if (!BodyScript.Ignition)
            return;

        BodyScript.Ignition = false;

        BodyScript.changeColorCost(cardColorInfo.緑, 1);
        if (!BodyScript.myCheckCost())
            return;

        BodyScript.setEffect(ID, player, Motions.PayCost);
        BodyScript.setEffect(ID, player, Motions.CostGoTrash);

        afterIgni = true;
 
    }

    void afterIgnition()
    {
        if (!afterIgni || BodyScript.effectTargetID.Count > 0)
            return;
        afterIgni = false;

        BodyScript.funcTargetIn(player, Fields.TRASH, check);
        BodyScript.setEffect(-2, 0, Motions.GoHand);
    }

    bool check(int x, int target)
    {
        return ManagerScript.checkColor(x, target, cardColorInfo.無色);
    }
}

using UnityEngine;
using System.Collections;

public class WD08_009 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    oneceTurnList once;
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
        trigger();

        receive();

        igni();

        burst();
	}

    void burst()
    {
        if (!BodyScript.isBursted())
            return;

        BodyScript.powerUpValue = -10000;
        BodyScript.funcTargetIn(1 - player, Fields.SIGNIZONE, triggerCheck);
        BodyScript.setEffect(-1, 0, Motions.PowerUpEndPhase);
    }

    void igni()
    {
        if (!BodyScript.TrashIgnition)
            return;

        BodyScript.TrashIgnition = false;

        BodyScript.changeColorCost(cardColorInfo.黒, 2);
        BodyScript.Cost[0] = 1;

        if (!BodyScript.myCheckCost())
            return;

        BodyScript.powerUpValue = -8000;
        BodyScript.funcTargetIn(1 - player, Fields.SIGNIZONE, triggerCheck);
        BodyScript.setEffect(-1, 0, Motions.PowerUpEndPhase);

        BodyScript.setEffect(ID, player, Motions.GoDeckBottom);
    }

    void trigger()
    {
        if (!BodyScript.isOnBattleField() || ManagerScript.getTrashSummonID() == -1 || ManagerScript.getTrashSummonID() / 50 != player)
            return;

        //taget
        BodyScript.funcTargetIn(1 - player, Fields.SIGNIZONE, triggerCheck);

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.Targetable.Clear();

        //cost
        BodyScript.changeColorCost((int)cardColorInfo.黒, 2);

        if (!BodyScript.myCheckCost())
            return;

        BodyScript.DialogFlag = true;
    }

    bool triggerCheck(int x,int target)
    {
        return true;
    }

    void receive()
    {
        //receive
        if (BodyScript.messages.Count == 0)
            return;

        if (BodyScript.messages[0].Contains("Yes"))
        {
            BodyScript.setEffect(ID, player, Motions.PayCost);

            BodyScript.funcTargetIn(1 - player, Fields.SIGNIZONE, triggerCheck);
            BodyScript.setEffect(-1, 0, Motions.EnaCharge);
        }

        BodyScript.messages.Clear();
    }
}

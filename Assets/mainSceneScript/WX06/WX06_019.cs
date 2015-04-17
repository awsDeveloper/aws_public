using UnityEngine;
using System.Collections;

public class WX06_019 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool burstInput = false;

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
        replaceCheck();

        receive();

        burst();

        inputReturn();

    }

    void replaceCheck()
    {
        if (!ManagerScript.checkClass(ManagerScript.RemovingID, cardClassInfo.精生_水獣)
            || !BodyScript.isOnBattleField()
            || ManagerScript.getEffecterNowID() == -1)
            return;

        int rID = ManagerScript.RemovingID;

        if (rID == ID + player * 50)
            return;

        BodyScript.DialogFlag = true;
        BodyScript.ReplaceFlag = true;
    }

    void receive()
    {
        //receive
        if (BodyScript.messages.Count == 0)
            return;

        if (BodyScript.messages[0].Contains("Yes"))
        {
            BodyScript.effectTargetID.Add(ID + 50 * player);
            BodyScript.effectMotion.Add((int)Motions.PowerUpEndPhase);
            BodyScript.powerUpValue = -6000;

            ManagerScript.Replace(BodyScript);
        }

        BodyScript.messages.Clear();

    }

    void burst()
    {
        if (!BodyScript.isBursted())
            return;

        int target = player;
        int f = (int)Fields.HAND;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (ManagerScript.checkClass(x,target, cardClassInfo.精生_水獣))
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.cursorCancel = true;
        burstInput = true;
        for (int i = 0; i < BodyScript.Targetable.Count; i++)
            BodyScript.setEffect(-1, 0, Motions.Open);
    }

    void inputReturn()
    {
        if (!burstInput || BodyScript.inputReturn == -1)
            return;

        int returnNun = BodyScript.inputReturn;

        burstInput = false;
        BodyScript.cursorCancel = false;
        BodyScript.inputReturn = -1;

        BodyScript.setEffect(0, player, Motions.CloseHand);

        int target = 1 - player;
        int f = (int)Fields.SIGNIZONE;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0)
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.powerUpValue = -3000 * returnNun;
        BodyScript.setEffect(-1, 0, Motions.PowerUpEndPhase);
    }
}

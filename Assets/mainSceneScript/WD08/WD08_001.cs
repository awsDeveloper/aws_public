using UnityEngine;
using System.Collections;

public class WD08_001 : MonoBehaviour {
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

        //dialog
        BodyScript.checkStr.Add("墓地肥し");
        BodyScript.checkStr.Add("蘇生");

        for (int i = 0; i < BodyScript.checkStr.Count; i++)
            BodyScript.checkBox.Add(false);

        once = gameObject.AddComponent<oneceTurnList>();
        once.addOnce();
    }
	
	// Update is called once per frame
	void Update () {
        cip();

        //ignition
        igni();

        receive();
	}

    void receive()
    {
        //receive
        if (BodyScript.messages.Count == 0)
            return;

        if (BodyScript.messages[0].Contains("Yes"))
        {
            if (BodyScript.checkBox[0]) effect_1();

            if (BodyScript.checkBox[1]) effect_2();
        }

        BodyScript.messages.Clear();
    }

    void igni()
    {
        if (!BodyScript.Ignition)
            return;

        BodyScript.Ignition = false;

        bool flag_1 = once.onceIsFalse(0);
        bool flag_2 = BodyScript.isUp();

        if (flag_1 && flag_2)
        {
            BodyScript.DialogFlag = true;
            BodyScript.DialogNum = 2;
            BodyScript.DialogCountMax = 1;
        }
        else if (flag_2)
            effect_2();
        else if (flag_1)
            effect_1();
    }

    void effect_1()
    {
        BodyScript.setEffect(0, player, Motions.TopGoTrash);
        BodyScript.setEffect(0, player, Motions.TopGoTrash);
        BodyScript.setEffect(0, player, Motions.TopGoTrash);

        once.onceUp(0);
    }


    void effect_2()
    {
        BodyScript.setEffect(ID, player, Motions.Down);

        BodyScript.funcTargetIn(player, Fields.TRASH, cipCheck);
        BodyScript.setEffect(-2, 0, Motions.Summon);
    }

    void cip()
    {
        if (!BodyScript.isCiped())
            return;

        BodyScript.funcTargetIn(player, Fields.TRASH, cipCheck);

        BodyScript.setEffect(-2, 0, Motions.TraIgniCostZeroEnd);
    }

    bool cipCheck(int x, int target)
    {
        return ManagerScript.checkType(x, target, cardTypeInfo.シグニ);
    }
}

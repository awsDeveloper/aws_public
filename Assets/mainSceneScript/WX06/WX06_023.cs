using UnityEngine;
using System.Collections;

public class WX06_023 : MonoBehaviour {
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
        ignition();
    }

    void ignition()
    {
        if (!BodyScript.Ignition)
            return;

        BodyScript.Ignition = false;

        if (ManagerScript.getIDConditionInt(ID, player) != (int)Conditions.Up)
            return;

        int target = player;
        int f = (int)Fields.MAINDECK;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0 && ManagerScript.checkClass(x,target, cardClassInfo.精像_天使) && !ManagerScript.checkName(x,target,"聖墓の神妹　ナキールン"))
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.setEffect(ID, player, Motions.Down);

        BodyScript.setEffect(-2, 0, Motions.GoHand);
    }
}

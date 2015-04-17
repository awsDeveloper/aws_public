using UnityEngine;
using System.Collections;

public class WD08_015 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool afterIgniFlag = false;

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
        Igni();

        afterIgni();

        burst();
    }

    void burst()
    {
        if (!BodyScript.isBursted())
            return;

        BodyScript.setEffect(0, player, Motions.Draw);
    }

    void Igni()
    {
        if (!BodyScript.Ignition)
            return;

        BodyScript.Ignition = false;

        if (!BodyScript.isUp())
            return;

        BodyScript.setEffect(ID, player, Motions.Down);

        BodyScript.setEffect(0, player, Motions.TopGoTrash);
        BodyScript.setEffect(0, player, Motions.TopGoTrash);
        BodyScript.setEffect(0, player, Motions.TopGoTrash);

        afterIgniFlag = true;
    }

    void afterIgni()
    {
        if (!afterIgniFlag || BodyScript.effectTargetID.Count > 0)
            return;

        afterIgniFlag = false;

        int index = 0;
        int count = 0;

        while (true)
        {
            int x = ManagerScript.getTopGoTrashListID(index, ID, player);

            if (x == -1)
                break;

            if (ManagerScript.checkClass(x, cardClassInfo.精械_古代兵器))
                count++;

            index++;
        }

        if (count < 3)
            return;

        BodyScript.setEffect(0, player, Motions.Draw);

    }
}

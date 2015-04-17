using UnityEngine;
using System.Collections;

public class WX05_080 : MonoBehaviour {
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
        checkIgni();
    }

    void checkIgni()
    {
        //ignition
        if (!BodyScript.Ignition)
            return;
        BodyScript.Ignition = false;

        if (ManagerScript.getIDConditionInt(ID, player) != 1)
            return;

        int target = player;
        int f = 3;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if (x >= 0 && x!=ID && checkClass(x,target))
            {
                int cID = ManagerScript.GetCharm(x, target);

                if (cID == -1)
                    BodyScript.Targetable.Add(x + 50 * target);
            }
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.effectFlag = true;
        BodyScript.effectTargetID.Add(ID + 50 * player);
        BodyScript.effectMotion.Add(8);

        BodyScript.setEffect(-1, 0, Motions.SetCharm);
        BodyScript.CharmizeID = ID + 50 * player;
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[0] == 4 && c[1] == 1);
    }
}

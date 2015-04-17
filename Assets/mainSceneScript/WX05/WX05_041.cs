using UnityEngine;
using System.Collections;

public class WX05_041 : MonoBehaviour
{
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
        //ignition
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;
            igni();
        }
    }

    void igni()
    {
        if (ManagerScript.getIDConditionInt(ID, player) != 1)
            return;

        BodyScript.effectFlag = true;
        BodyScript.effectTargetID.Add(ID + 50 * player);
        BodyScript.effectMotion.Add((int)Motions.Down);

        int target = player;
        for (int i = 0; i < 3; i++)
        {
            int x = ManagerScript.getFieldRankID(3, i, target);
            if (checkClass(x,target))
                BodyScript.Targetable.Add(x + 50 * (target));
        }

        if (BodyScript.Targetable.Count > 0)
            BodyScript.setEffect(-1, 0, Motions.EnLancerEnd);
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[0] == 5 && c[1] == 1) || (c[0] == 5 && c[1] == 2);
    }
}

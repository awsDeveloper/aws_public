using UnityEngine;
using System.Collections;

public class WX05_049 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

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

            if (ManagerScript.getIDConditionInt(ID, player) == 1)
            {
                BodyScript.Cost[0] = 1;
                BodyScript.Cost[1] = 2;
                BodyScript.Cost[2] = 0;
                BodyScript.Cost[3] = 0;
                BodyScript.Cost[4] = 0;
                BodyScript.Cost[5] = 0;

                if (ManagerScript.checkCost(ID, player))
                {

                    BodyScript.effectFlag = true;
                    BodyScript.effectTargetID.Add(ID + 50 * player);
                    BodyScript.effectMotion.Add(17);
                    BodyScript.effectTargetID.Add(ID + 50 * player);
                    BodyScript.effectMotion.Add(65);

                    int target = player;
                    int f = (int)Fields.MAINDECK;
                    int num = ManagerScript.getNumForCard(f, target);

                    for (int i = 0; i < num; i++)
                    {
                        int x = ManagerScript.getFieldRankID(f, i, target);
                        if (x >= 0 && checkClass(x, target))
                            BodyScript.Targetable.Add(x + 50 * (target));
                    }

                    if (BodyScript.Targetable.Count > 0)
                    {
                        BodyScript.GUIcancelEnable = true;

                        for (int i = 0; i < 2 && i<BodyScript.Targetable.Count; i++)
                            BodyScript.setEffect(-2, 0, Motions.GoHand);
                    }
                }
            }
        }
        //UpDate
        field = ManagerScript.getFieldInt(ID, player);
    }

    bool checkClass(int x, int cplayer)
    {
        if (ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_アーム))
            return true;

        return ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_ウェポン);
    }
}

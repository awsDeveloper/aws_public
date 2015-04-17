using UnityEngine;
using System.Collections;

public class WX05_029 : MonoBehaviour {
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

            if (ManagerScript.getIDConditionInt(ID, player) == 1 && ManagerScript.getCardPower(ID,player)>=12000)
            {
                BodyScript.Cost[0] = 0;
                BodyScript.Cost[1] = 1;
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
                    BodyScript.effectMotion.Add((int)Motions.Down);

                    int target = 1 - player;
                    for (int i = 0; i < 3; i++)
                    {
                        int x = ManagerScript.getFieldRankID(3, i, target);
                        if (x >= 0)
                        {
                            BodyScript.Targetable.Add(x + 50 * (target));
                        }
                    }
                    if (BodyScript.Targetable.Count > 0)
                    {
                        BodyScript.effectFlag = true;
                        BodyScript.effectTargetID.Add(-1);
                        BodyScript.effectMotion.Add((int)Motions.GoTrash);
                    }
                }
            }
        }
    }
}

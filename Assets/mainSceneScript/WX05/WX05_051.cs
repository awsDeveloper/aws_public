using UnityEngine;
using System.Collections;

public class WX05_051 : MonoBehaviour {
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
        //moyasi
        if (ManagerScript.getBanishedID() == ID + player * 50)
        {
            BodyScript.Cost[0] = 0;
            BodyScript.Cost[1] = 1;
            BodyScript.Cost[2] = 0;
            BodyScript.Cost[3] = 0;
            BodyScript.Cost[4] = 0;
            BodyScript.Cost[5] = 0;

            if (ManagerScript.checkCost(ID, player))
            {
                BodyScript.DialogFlag = true;
            }
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add(2);
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes") && ManagerScript.checkCost(ID, player))
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(ID + 50 * player);
                BodyScript.effectMotion.Add(17);

                int target = player;
                int f = 0;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = num - 1; i >= 0; i--)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);
                    if (x >= 0 && ManagerScript.getCardScr(x, target).Name.Contains("中槍　ブルナク"))
                    {
                        BodyScript.Targetable.Add(x + 50 * target);
                    }
                }

                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add(16);
                }
            }

            BodyScript.messages.Clear();
        }

        //UpDate
        field = ManagerScript.getFieldInt(ID, player);
    }
}

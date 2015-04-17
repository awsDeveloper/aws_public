using UnityEngine;
using System.Collections;

public class WX01_085sc : MonoBehaviour
{
    GameObject Manager;
    DeckScript ManagerScript;
    GameObject Body;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;
    int count = 1;
    bool chantFlag = false;

    // Use this for initialization
    void Start()
    {
        Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //chant
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            for (int i = 0; i < 3; i++)
            {
                int x = ManagerScript.getFieldRankID(3, i, 1 - player);
                if (x >= 0)
                    BodyScript.setEffect(x, 1 - player, Motions.DownAndFreeze);
            }
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            int target = 1 - player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);

                if (x >= 0)
                    BodyScript.Targetable.Add(x + target * 50);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                for (int i = 0; i < 2 && i<BodyScript.Targetable.Count; i++)
                    BodyScript.setEffect(-1, 0, Motions.DownAndFreeze);
            }
        }

        //update
        field = ManagerScript.getFieldInt(ID, player);
    }
}

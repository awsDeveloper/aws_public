using UnityEngine;
using System.Collections;

public class WD03_014sc : MonoBehaviour
{
    GameObject Manager;
    DeckScript ManagerScript;
    GameObject Body;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;
    bool DrawFlag = false;

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
        if (ManagerScript.getFieldInt(ID, player) == 3 && field != 3)
        {
            BodyScript.effectFlag = true;
            DrawFlag = true;
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add(2);
        }
        if (BodyScript.effectTargetID.Count == 0 && DrawFlag)
        {
            DrawFlag = false;
            int handNum = ManagerScript.getFieldAllNum(2, player);
            if (handNum > 0)
            {
                for (int i = 0; i < handNum; i++)
                {
                    int x = ManagerScript.getFieldRankID(2, i, player);
                    BodyScript.Targetable.Add(x + 50 * player);
                }
                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.effectTargetID.Add(-1);
                    BodyScript.effectMotion.Add(19);
                }
            }
        }
        field = ManagerScript.getFieldInt(ID, player);
    }
}

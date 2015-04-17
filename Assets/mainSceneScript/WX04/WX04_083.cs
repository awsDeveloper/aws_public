using UnityEngine;
using System.Collections;

public class WX04_083 : MonoBehaviour
{
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
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            BodyScript.effectFlag = true;

            for (int i = 0; i < 5 && i < ManagerScript.getFieldAllNum(0, player); i++)
            {
                BodyScript.effectTargetID.Add(50 * player);
                BodyScript.effectMotion.Add(2);
            }
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add(45);
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add(45);
        }
        field = ManagerScript.getFieldInt(ID, player);
    }
}

using UnityEngine;
using System.Collections;

public class WX02_069 : MonoBehaviour
{
    GameObject Manager;
    DeckScript ManagerScript;
    GameObject Body;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    // Use this for initialization
    void Start()
    {
        Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;
        BodyScript.powerUpValue = -5000;
        Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BodyScript.TrashIgnition)
        {
            BodyScript.TrashIgnition = false;

            BodyScript.Cost[0] = 0;
            BodyScript.Cost[1] = 0;
            BodyScript.Cost[2] = 0;
            BodyScript.Cost[3] = 0;
            BodyScript.Cost[4] = 0;
            BodyScript.Cost[5] = 2;

            if (ManagerScript.checkCost(ID, player) && ManagerScript.getFieldAllNum(3, player) < 3)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(ID + player * 50);
                BodyScript.effectMotion.Add(17);
                BodyScript.effectTargetID.Add(ID + 50 * player);
                BodyScript.effectMotion.Add(6);
            }
        }
    }
}

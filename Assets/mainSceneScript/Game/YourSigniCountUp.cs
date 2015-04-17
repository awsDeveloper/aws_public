using UnityEngine;
using System.Collections;

public class YourSigniCountUp : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool upFlag = false;

    public int puv = 4000;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.powerUpValue = puv;
    }

    // Update is called once per frame
    void Update()
    {
        //check situation
        if (BodyScript.isOnBattleField() && getNum() > 0)
        {
            upFlag = true;
            ManagerScript.powChanListChangerClear(ID, player);
            ManagerScript.alwaysChagePower(ID, player, BodyScript.powerUpValue * getNum(), ID, player);
        }
        else if (upFlag)
        {
            upFlag = false;
            ManagerScript.powChanListChangerClear(ID, player);
        }
    }

    int getNum()
    {
        return ManagerScript.getFieldAllNum((int)Fields.SIGNIZONE,1-player);
    }
}

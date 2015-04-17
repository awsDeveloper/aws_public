using UnityEngine;
using System.Collections;

public class EnDoubleCrashScr : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;
    bool equipFlag = false;

    public string masterSerial = "";

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
        //check equip
        if(checkEquip())
        {
            if (!equipFlag && !BodyScript.DoubleCrash)
            {
                equipFlag = true;
                BodyScript.DoubleCrash = true;
            }
        }
        else
        {
            dequip();
            Destroy(this);
        }

        //エンド自爆
/*        if (ManagerScript.getPhaseInt() == 7)
        {
            dequip();
            Destroy(this);
        }*/
    }

    bool checkEquip()
    {
        int lID = ManagerScript.getLrigID(player);

        return ManagerScript.getSerialNum(lID, player) == masterSerial;

    }

    void dequip()
    {
        if (equipFlag)
        {
            equipFlag = false;
            BodyScript.DoubleCrash = false;
        }
    }
}

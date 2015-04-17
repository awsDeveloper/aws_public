using UnityEngine;
using System.Collections;

public class enMultiEnaScr : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;
    bool equipFlag = false;

    public int masterID = -1;

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
        if (!checkEquip())
        {
            dequip();
            Destroy(this);
            return;
        }

        //check equip
        if (ManagerScript.getFieldInt(ID, player) == (int)Fields.ENAZONE && !ManagerScript.getCardScr(masterID%50,masterID/50).lostEffect)
        {
            if (!BodyScript.MultiEnaFlag)
            {
                equipFlag = true;
                BodyScript.MultiEnaFlag = true;
            }
        }
        else
            dequip();

    }

    bool checkEquip()
    {
        if(masterID < 0)
            return false;

        int x = masterID % 50;
        int target = masterID / 50;        
        

        if (ManagerScript.getCardType(x, target) == 0)
        {
            int lID = ManagerScript.getLrigID(target);
            return x == lID;
        }
        else
            return ManagerScript.getFieldInt(x, target) == (int)Fields.SIGNIZONE;
    }

    void dequip()
    {
        if (equipFlag)
        {
            equipFlag = false;
            BodyScript.MultiEnaFlag = false;
        }
    }
}

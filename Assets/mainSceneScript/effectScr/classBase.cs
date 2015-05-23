using UnityEngine;
using System.Collections;

public class classBase : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool upFlag = false;
    classList myClass = new classList();

    public int baseValue = 8000;


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
        if (ManagerScript.getFieldInt(ID, player) == 3 && myClass.isClassSigniOnBattleField(player,BodyScript))
        {
            if (!upFlag)
            {
                ManagerScript.changeBasePower(ID, player, baseValue);
                upFlag = true;
            }
        }
        else if (upFlag)
        {
            ManagerScript.changeBasePower(ID, player, BodyScript.OriginalPower);
            upFlag = false;
        }

    }

    public void setClass(cardClassInfo info)
    {
        myClass.setClass(info);
    }
}

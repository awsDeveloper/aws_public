using UnityEngine;
using System.Collections;

public class WX02_052sc : MonoBehaviour
{
    GameObject Manager;
    DeckScript ManagerScript;
    GameObject Body;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool upFlag = false;
    // Use this for initialization
    void Start()
    {
        Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;
        BodyScript.powerUpValue = 3000;

        Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();
    }

    // Update is called once per frame
    void Update()
    {
        bool flag = false;
        for (int i = 0; i < 3; i++)
        {
            int x = ManagerScript.getFieldRankID(3, i, player);
            if (x >= 0 && x != ID)
            {
                //				int[] c=ManagerScript.getCardClass(x,player);
                //				if(c[0]==1 && c[1]==1)flag=true;
                if (ManagerScript.checkClass(x, player, cardClassInfo.精武_ウェポン))
                    flag = true;
            }
        }
        if (ManagerScript.getFieldInt(ID, player) == 3 && flag)
        {
            if (!upFlag)
            {
                ManagerScript.upCardPower(ID, player, BodyScript.powerUpValue);
                upFlag = true;
            }
        }
        else if (upFlag)
        {
            ManagerScript.upCardPower(ID, player, -BodyScript.powerUpValue);
            upFlag = false;
        }

    }
}

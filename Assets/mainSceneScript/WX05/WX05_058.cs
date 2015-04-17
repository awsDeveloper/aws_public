using UnityEngine;
using System.Collections;

public class WX05_058 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    public int checkClass_1 = 5;
    public int checkClass_2 = 3;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.powerUpValue = 2000;
    }

    // Update is called once per frame
    void Update()
    {
        checkEquip();
    }

    void checkEquip()
    {
        //check situation
        if (ManagerScript.getFieldInt(ID, player) == 3)
        {
            ManagerScript.powChanListChangerClear(ID, player);
            ManagerScript.alwaysChagePower(ID, player, getOtherClassLevelSum()*1000, ID, player);
        }
        else
            ManagerScript.powChanListChangerClear(ID, player);
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return c[0] == checkClass_1 && c[1] == checkClass_2;
    }

    int getOtherClassLevelSum()
    {
        int sum = 0;

        int target = player;
        int f = 3;
        int num = ManagerScript.getNumForCard(f, target);

        //check exist in upList
        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0 && checkClass(x, target) && ID != x)
                sum += ManagerScript.getCardLevel(x, target);
        }

        return sum;
    }
}

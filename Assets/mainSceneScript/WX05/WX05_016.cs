using UnityEngine;
using System.Collections;

public class WX05_016 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool afterPayCost = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.SpellCutIn = true;

        BodyScript.PayedCostEnable = true;
    }
    // Update is called once per frame
    void Update()
    {
        checkPayedCost();

        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag && afterPayCost)
        {
            afterPayCost = false;

            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50 * (1 - player));
            BodyScript.effectMotion.Add((int)Motions.GoNextTurn);
        }

        //update
        field = ManagerScript.getFieldInt(ID, player);
    }

    void checkPayedCost()
    {
        bool[] flag = new bool[6];
        int multi=0;

        while (BodyScript.PayedCostList.Count > 0)
        {
            int x = BodyScript.PayedCostList[0];
            int target = x / 50;
            x = x % 50;

            if (ManagerScript.getCardScr(x, target).MultiEnaFlag)
                multi++;
            else
                flag[ManagerScript.getCardColor(x, target)] = true;

            BodyScript.PayedCostList.RemoveAt(0);
        }

        for (int i = 1; i < flag.Length; i++)
        {
            if (!flag[i])
                multi--;
        }

        if (multi >= 0)
            afterPayCost = true;
    }
}

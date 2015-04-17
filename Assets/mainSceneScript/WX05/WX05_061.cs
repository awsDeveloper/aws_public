using UnityEngine;
using System.Collections;

public class WX05_061 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;
    bool costFlag = false;
    bool afterGohand = false;

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
        //cip
        if (ManagerScript.getFieldInt(ID, player) == 3 && field != 3 && !BodyScript.BurstFlag)
        {
            int target = player;
            int f = 2;
            int num = ManagerScript.getFieldAllNum(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0 && checkClass(x, target))
                {
                    BodyScript.Targetable.Add(x + 50 * target);
                }
            }

            if (BodyScript.Targetable.Count > 0)
                BodyScript.DialogFlag = true;

        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add((int)Motions.CostGoTrash);
                costFlag = true;
            }
            else BodyScript.Targetable.Clear();

            BodyScript.messages.Clear();
        }

        //after cost
        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;
            BodyScript.Targetable.Clear();

            int target = player;
            int f = (int)Fields.MAINDECK;
            int num = ManagerScript.getFieldAllNum(f, target);

            for (int i = 0; i < 5; i++)
            {
                int x = ManagerScript.getFieldRankID(f, num-1-i, target);
                if (x >= 0)
                {
                    afterGohand = true;
                    BodyScript.setEffect(x, target, Motions.GoShowZone);
                    
                    if(checkClass(x,target))
                        BodyScript.Targetable.Add(x + 50 * target);
                }
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.setEffect(-1, 0, Motions.DontShuffleGoHand);
            }
        }

        if (afterGohand && BodyScript.effectTargetID.Count == 0)
        {
            afterGohand = false;

            for (int i= 0; true; i++)
            {
                int x=ManagerScript.getShowZoneID(i);

                if (x < 0)
                    break;

                BodyScript.Targetable.Add(x);
            }

            for (int i = 0; i < BodyScript.Targetable.Count; i++)
                BodyScript.setEffect(-1, 0, Motions.GoDeckBottom);
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
            BodyScript.setEffect(0, player, Motions.TopEnaCharge);

        //update
        field = ManagerScript.getFieldInt(ID, player);
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[0] == 2 && c[1] == 3);
    }
}

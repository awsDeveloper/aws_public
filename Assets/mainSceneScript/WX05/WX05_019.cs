using UnityEngine;
using System.Collections;

public class WX05_019 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool upFlag = false;
    bool lostFlag = false;
    int lastFID = -1;
    int field = -1;

    bool afterBurst = false;
    bool NextAfterBurst = false;
    int[] lostIDs = new int[] { -1, -1, -1 };

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.powerUpValue = 3000;
    }

    // Update is called once per frame
    void Update()
    {
        //always
        if (ManagerScript.getFieldInt(ID, player) == 3)
        {
            int fID = ManagerScript.getSigniFront(ID, player);
            if (fID >= 0 && !ManagerScript.getCardScr(fID, 1 - player).lostEffect)
            {
                ManagerScript.changeLostEffect(fID, 1 - player, true, ID, player);
                lostFlag = true;
            }
        }
        else if (lostFlag)
        {
            lostFlag = false;
            ManagerScript.changeLostEffect(lastFID, 1 - player, false, ID, player);
        }
 
        //always2
        if (lostEffNum(1 - player) >= 2 && ManagerScript.getFieldInt(ID, player) == 3)
        {
            if (!upFlag)
            {
                    upFlag = true;
                    ManagerScript.alwaysChagePower(ID, player, BodyScript.powerUpValue, ID, player);
            }
        }
        else if (upFlag)
        {
            upFlag = false;
            ManagerScript.powChanListChangerClear(ID, player);
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field!=8 && BodyScript.BurstFlag) 
        {
            BodyScript.effectFlag = true;
            BodyScript.effectMotion.Add((int)Motions.Draw);
            BodyScript.effectTargetID.Add(50 * player);

            NextAfterBurst = true;
        }

        //up afterBurst
        if (NextAfterBurst && ManagerScript.getTurnStartFlag())
        {
            NextAfterBurst = false;
            afterBurst = true;
        }

        //down after burst
        if (afterBurst && ManagerScript.getTurnEndFlag())
        {
            afterBurst = false;

            for (int i = 0; i < lostIDs.Length; i++)
            {
                if (lostIDs[i] >= 0)
                {
                    CardScript sc = ManagerScript.getCardScr(lostIDs[i], 1 - player);
                    if (sc != null && sc.lostEffect)
                        ManagerScript.changeLostEffect(lostIDs[i], 1 - player, false, ID, player);

                    lostIDs[i] = -1;
                }
            }
        }

        //after bust
        if (afterBurst)
        {
            int target = 1 - player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num && i<lostIDs.Length; i++)
            {
                int gfrID=ManagerScript.getFieldRankID(f,i,target);

                if (gfrID >= 0 && !ManagerScript.getCardScr(gfrID, target).lostEffect)
                {
                    lostIDs[i] = gfrID;
                    ManagerScript.changeLostEffect(lostIDs[i], target, true, ID, player);
                }

            }
        }

        //triggered
        if (ManagerScript.getFieldInt(ID, player) == 3)
        {
            int tID = ManagerScript.getExitID((int)Fields.SIGNIZONE, (int)Fields.MAINDECK);

            if (tID >= 0 && tID / 50 == 1 - player)
            {
                int target =player;
                int f = (int)Fields.MAINDECK;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);

                    if (x >= 0 && checkClass(x, target))
                        BodyScript.Targetable.Add(x + 50 * target);
                }

                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.effectFlag = true;
                    BodyScript.effectMotion.Add((int)Motions.GoHand);
                    BodyScript.effectTargetID.Add(-2);
                }

            }
        }

        //update
        lastFID = ManagerScript.getSigniFront(ID, player);
        field = ManagerScript.getFieldInt(ID, player);
    }

    int lostEffNum(int target)
    {
        int count = 0;

        int f = 3;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if (x >= 0)
            {
                CardScript sc = ManagerScript.getCardScr(x, target);

                if (sc.lostEffect || sc.Text == "")
                    count++;
            }
        }

        return count;
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0)
            return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[0] == 3 && c[1] == 2);
    }
   
}

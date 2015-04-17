using UnityEngine;
using System.Collections;

public class WX05_006 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool costFlag = false;
    int tgColor = -1;
    int tgID = -1;

    bool upFlag = false;
 
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
        BodyScript.useLimit = uselim();

        //cip
        if (ManagerScript.getCipID() == ID + player * 50)
        {
            //equip multiEna
            int target = player;
            for (int i = 0; i < 50; i++)
            {
                GameObject obj = ManagerScript.getFront(i, target);

                obj.AddComponent<enMultiEnaScr>();
                enMultiEnaScr[] scr = obj.GetComponents<enMultiEnaScr>();

                foreach (var item in scr)
                {
                    if (item.masterID == -1)
                    {
                        item.masterID = ID + player * 50;
                        break;
                    }
                }
            }
        }

        //虚無フラグ
        if (ManagerScript.getLrigID(player) == ID && !BodyScript.lostEffect)
        {
            if (!ManagerScript.kyomuFlag[player])
            {
                upFlag = true;
                ManagerScript.kyomuFlag[player] = true;
            }
        }
        else if (upFlag)
        {
            upFlag = false;
            ManagerScript.kyomuFlag[player] = false;
        }

        //ignition
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            ManagerScript.targetableExceedIn(player, BodyScript);

            int exNum = 5;

            if (BodyScript.Targetable.Count >= exNum)
            {
                for (int i = 0; i < exNum; i++)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add((int)Motions.Exceed);
                }

                costFlag = true;
                BodyScript.effectFlag = true;
            }
            else
                BodyScript.Targetable.Clear();
        }

        //igni after cost
        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;

            int target = player;
            int f = 2;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);

                if (x >= 0)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectMotion.Add((int)Motions.AntiCheck);
                BodyScript.effectTargetID.Add(-1);

                BodyScript.TargetIDEnable = true;
                BodyScript.effectSelecter = player;
            }
        }

        //input 1
        if (BodyScript.TargetID.Count > 0)
        {
            tgID = BodyScript.TargetID[0];
            tgColor = ManagerScript.getCardColor(tgID % 50, tgID / 50);

            BodyScript.TargetID.Clear();
            BodyScript.TargetIDEnable = false;

            BodyScript.DialogFlag = true;
            BodyScript.DialogNum = 3;
            BodyScript.effectSelecter = 1 - player;
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            int count = int.Parse(BodyScript.messages[0]);
            BodyScript.messages.Clear();

            BodyScript.effectMotion.Add((int)Motions.SetAnimation);
            BodyScript.effectTargetID.Add(tgID);

            if (count != tgColor && tgID >= 0)
            {
                int target =1- player;
                int f = 3;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);

                    if (x >= 0)
                    {
                        BodyScript.effectMotion.Add((int)Motions.GoTrash);
                        BodyScript.effectTargetID.Add(x + 50 * target);
                    }
                }
            }
        }
    }

    bool uselim()
    {
        int target = player;
        int f = (int)Fields.ENAZONE;
        int num = ManagerScript.getNumForCard(f, target);

        bool[] colors = new bool[6];

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0)
                colors[ ManagerScript.getCardColor(x,target)]=true;
        }

        int c = 0;
        for (int i = 1; i < colors.Length; i++)
            if (colors[i])
                c++;

        return c < 3;
    }
}

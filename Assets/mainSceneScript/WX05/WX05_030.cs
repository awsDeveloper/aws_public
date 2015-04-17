using UnityEngine;
using System.Collections;

public class WX05_030 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool bFlag = false;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.GUIcancelEnable = true;
    }

    // Update is called once per frame
    void Update()
    {
        //chant
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            int target = player;
            int f = (int)Fields.HAND;
            int num = ManagerScript.getFieldAllNum(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (checkClass(x, target))
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.setEffect(-1, 0, Motions.Open);
                BodyScript.TargetIDEnable = true;
            }
        }

        //after open
        if (BodyScript.TargetID.Count > 0 && BodyScript.effectTargetID.Count==0)
        {
            BodyScript.Targetable.Clear();

            int t = BodyScript.TargetID[0];
            string tName = ManagerScript.getCardScr(t % 50, t / 50).Name;

            BodyScript.TargetID.RemoveAt(0);
            BodyScript.TargetIDEnable = false;

            int target = player;
            int f = 0;
            int num = ManagerScript.getFieldAllNum(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if(ManagerScript.getCardScr(x,target).Name == tName)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                for (int i = 0; i < 3 && i < BodyScript.Targetable.Count; i++)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add(16);
                }
            }

            BodyScript.setEffect(t%50, t/50, Motions.Close);
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            int target = player;
            int f = (int)Fields.HAND;
            int num = ManagerScript.getFieldAllNum(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (checkClass(x, target))
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
                BodyScript.DialogFlag = true;
        }

        //after burst
        if (bFlag && BodyScript.effectTargetID.Count == 0)
        {
            bFlag = false;

            int target = player;
            int f = 0;
            int num = ManagerScript.getFieldAllNum(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (checkClass(x, target))
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                for (int i = 0; i < 2 && i < BodyScript.Targetable.Count; i++)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add(16);
                }
            }
        }
        

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                BodyScript.setEffect(-1, 0, Motions.GoTrash);
                bFlag = true;
            }

            BodyScript.messages.Clear();
        }

        //update
        field = ManagerScript.getFieldInt(ID, player);
    }

    bool checkClass(int x, int cplayer)
    {
        return ManagerScript.checkClass(x, cplayer, cardClassInfo.精武_アーム);
    }
}

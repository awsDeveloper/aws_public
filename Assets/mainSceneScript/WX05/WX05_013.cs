using UnityEngine;
using System.Collections;

public class WX05_013 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool costFlag = false;
 
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
        //useLim
        int nowLrigID = ManagerScript.getLrigID(player);
        if (nowLrigID >= 0)
            BodyScript.useLimit = !ManagerScript.getCardScr(nowLrigID, player).Name.Contains("アン");

        //cip
        if (ManagerScript.getCipID() == ID + 50 * player)
        {
            int target = player;
            int f = (int)Fields.TRASH;
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

                for (int i = 0; i < 3 && i < BodyScript.Targetable.Count; i++)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add(16);
                }

                BodyScript.GUIcancelEnable = true;
            }
        }

        //ignition
        if (ManagerScript.getLrigID(player) == ID && BodyScript.Ignition)
        {
            BodyScript.Ignition = false;

            ManagerScript.targetableExceedIn(player, BodyScript);

            if (BodyScript.Targetable.Count >= 3)
            {
                BodyScript.effectFlag = true;

                for (int i = 0; i < 3; i++)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add((int)Motions.Exceed);
                }

                BodyScript.effectTargetID.Add(50 * player);
                BodyScript.effectMotion.Add((int)Motions.OpenHand);

                costFlag = true;
            }
            else
                BodyScript.Targetable.Clear();
        }

        //ignition after cost
        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;

            if (getClassNameNum(player) >= 8 && ManagerScript.getLastMotionsRear()==(int)Motions.OpenHand)
            {
                int target = 1-player;
                int f = 3;
                int num = ManagerScript.getNumForCard(f, target);

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);
                    if (x >= 0)
                    {
                        BodyScript.effectFlag = true;
                        BodyScript.effectTargetID.Add(x+50*target);
                        BodyScript.effectMotion.Add(16);
                    }
                }
            }

            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add((int)Motions.CloseHand);
        }

        //stop attack
        if (ManagerScript.getAttackerID() != -1 && ManagerScript.getAttackerID() / 50 == 1 - player && ManagerScript.getLrigID(player) == ID)
        {
            ManagerScript.targetableExceedIn(player,BodyScript);

            if (BodyScript.Targetable.Count >= 2)
                BodyScript.DialogFlag = true;
            else
                BodyScript.Targetable.Clear();
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {
                BodyScript.effectFlag = true;

                for (int i = 0; i < 2; i++)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add((int)Motions.Exceed);
                }

                BodyScript.effectTargetID.Add(50 * player);
                BodyScript.effectMotion.Add((int)Motions.stopAttack);
            }

            BodyScript.messages.Clear();
        }
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0)
            return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[0] == 4 && c[1] == 2);
    }

    int getClassNameNum(int target)
    {
        int num = 0;

        int f = 2;
        int max = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < max; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0 && checkClass(x, target))
            {

                string name = ManagerScript.getCardScr(x, target).Name;
                bool flag = false;

                for (int j = i + 1; j < max; j++)
                {
                    int y = ManagerScript.getFieldRankID(f, j, target);

                    if (name == ManagerScript.getCardScr(y, target).Name)
                        flag = true;
                }

                if (!flag)
                    num++;
            }
        }
        return num;
    }
}

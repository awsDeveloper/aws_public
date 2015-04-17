using UnityEngine;
using System.Collections;

public class WX05_015 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;
        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.attackArts = true;
    }

    // Update is called once per frame
    void Update()
    {
        //chant
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            int target = player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0 && checkClass(x, target))
                    BodyScript.Targetable.Add(x+50*target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-1);
                BodyScript.effectMotion.Add((int)Motions.GoTrash);

                BodyScript.TargetIDEnable = true;
            }
        }

        if (BodyScript.TargetID.Count > 0 && BodyScript.effectTargetID.Count==0)
        {
            int tID = BodyScript.TargetID[0];
            BodyScript.TargetID.RemoveAt(0);
            BodyScript.TargetIDEnable = false;


            if (ManagerScript.getLastMotionsRear() == (int)Motions.GoTrash)
            {
                int tLev = ManagerScript.getCardLevel(tID % 50, tID / 50);

                int target = 1 - player;
                int f = 3;
                int num = ManagerScript.getNumForCard(f, target);

                BodyScript.powerUpValue = -tLev * 1000;

                for (int i = 0; i < num; i++)
                {
                    int x = ManagerScript.getFieldRankID(f, i, target);
                    if (x >= 0)
                    {
                        BodyScript.effectTargetID.Add(x + 50 * target);
                        BodyScript.effectMotion.Add((int)Motions.PowerUpEndPhase);
                    }
                }
            }
            
        }

        //UpDate
        field = ManagerScript.getFieldInt(ID, player);
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0)
            return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[0] == 1 && c[1] == 2);
    }
}

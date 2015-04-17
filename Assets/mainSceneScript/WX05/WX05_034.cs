using UnityEngine;
using System.Collections;

public class WX05_034 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool chantFlag = false;

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
        BodyScript.changeCost(new int[]{ ManagerScript.getFieldAllNum((int)Fields.LIFECLOTH,player), 0, 1, 0, 0, 0 });

        //chant
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            BodyScript.setEffect(0, player, Motions.AntiCheck);
            BodyScript.AntiCheck = true;
            chantFlag = true;
        }

        if(chantFlag && BodyScript.effectTargetID.Count==0)
        {
            chantFlag = false;

            if (!BodyScript.AntiCheck)
            {
                int target = player;
                for (int i = 0; i < 50; i++)
                {
                    int x = i;

                    if (checkClass(x, target))
                    {
                        GameObject obj = ManagerScript.getFront(x, target);

                        if (obj != null)
                            obj.AddComponent<WX05_034_en>();
                    }
                }
            }

            BodyScript.AntiCheck = false;
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            BodyScript.setEffect(0, player, Motions.AntiCheck);

            int target = player;
            for (int i = 0; i < 50; i++)
            {
                int x = i;

                if (ManagerScript.getCardType(x,target)==2)
                {
                    GameObject obj = ManagerScript.getFront(x, target);

                    if (obj != null)
                        obj.AddComponent<WX05_034_en2>();
                }
            }
        }

        //update
        field = ManagerScript.getFieldInt(ID, player);
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[1] == 0 || c[1] == 1);
    }
}

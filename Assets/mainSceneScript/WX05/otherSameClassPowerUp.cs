using UnityEngine;
using System.Collections;

public class otherSameClassPowerUp : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    classPowerUp cpu;

    bool started = false;

    // Use this for initialization
    void Start()
    {
        if (started)
            return;

        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();


        if(gameObject.GetComponent<classPowerUp>() == null)
            gameObject.AddComponent<classPowerUp>();
        cpu = gameObject.GetComponent<classPowerUp>();

        started = true;
    }

    // Update is called once per frame
    void Update()
    {
        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            int target = player;
            int f = 0;
            int num = ManagerScript.getFieldAllNum(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (checkClass(x, target))
                {
                    BodyScript.Targetable.Add(x + 50 * target);
                }
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-2);
                BodyScript.effectMotion.Add(16);
            }
        }

        field = ManagerScript.getFieldInt(ID, player);
    }

    bool checkClass(int x, int cplayer)
    {
        return cpu.checkClass(x, cplayer);
    }

    public void setClassList(int c1, int c2)
    {
        Start();
        cpu.setClassList(c1, c2);
    }
}

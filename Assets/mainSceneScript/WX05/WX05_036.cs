using UnityEngine;
using System.Collections;

public class WX05_036 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

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
        //ignition
        if (BodyScript.Ignition)
        {
            BodyScript.Ignition = false;
            igni();
        }

        //after cost
        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;

            BodyScript.setEffect(0, player, Motions.Draw);
            BodyScript.setEffect(0, player, Motions.Draw);

            BodyScript.setEffect(0, player, Motions.oneHandDeath);
        }

        //always
        if (ManagerScript.getFieldInt(ID, player) == 3 && ManagerScript.getStartedPhase() == (int)Phases.AttackPhase && ManagerScript.getTurnPlayer()==player)
        {
            int target = player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);

                if (checkClass(x, target) && ManagerScript.getIDConditionInt(x, target) == (int)Conditions.Down)
                    BodyScript.setEffect(x, target, Motions.Up);
            }
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            BodyScript.setEffect(0, player, Motions.oneHandDeath);

            BodyScript.effectFlag = true;

            for (int i = 0; i < 2; i++)
                BodyScript.setEffect(0, 1 - player, Motions.RandomHandDeath);
        }

        //update
        field = ManagerScript.getFieldInt(ID, player);
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[0] == 5 && c[1] == 0);
    }

    void igni()
    {
        int target = player;
        int f = 3;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if (checkClass(x, target) && ManagerScript.getIDConditionInt(x, target) == (int)Conditions.Up)
                BodyScript.setEffect(x, target, Motions.Down);
        }

        //3体いないと終了
        if (BodyScript.effectTargetID.Count != 3)
        {
            BodyScript.effectFlag = false;
            BodyScript.effectTargetID.Clear();
            BodyScript.effectMotion.Clear();
            return;
        }

        costFlag = true;
    }
}

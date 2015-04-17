using UnityEngine;
using System.Collections;

public class WX05_026 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool onceTurn = false;
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

    }
    // Update is called once per frame
	void Update () {
        //always
        int tsID = ManagerScript.getTrashSummonID();
        if (ManagerScript.getFieldInt(ID, player) == 3 && tsID >= 0 && tsID / 50 == player && checkClass(tsID % 50, tsID / 50) && !onceTurn)
        {
            int target = player;
            int f = (int)Fields.TRASH;
            int num = ManagerScript.getFieldAllNum(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x>=0)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                onceTurn = true;

                BodyScript.effectFlag = true;
                BodyScript.effectMotion.Add((int)Motions.GoHand);
                BodyScript.effectTargetID.Add(-2);
            }
        }

        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            int target = player;
            int f = (int)Fields.MAINDECK;
            int num = ManagerScript.getFieldAllNum(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0 && ManagerScript.getCardType(x, target) == 2)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectMotion.Add((int)Motions.GoTrash);
                BodyScript.effectTargetID.Add(-2);

                BodyScript.effectMotion.Add((int)Motions.Shuffle);
                BodyScript.effectTargetID.Add(50*target);

                bFlag = true;
            }
        }
        //after burst
        if (bFlag && BodyScript.effectTargetID.Count == 0)
        {
            bFlag = false;
            BodyScript.Targetable.Clear();

            int target = player;
            int f = (int)Fields.TRASH;
            int num = ManagerScript.getFieldAllNum(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0)
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectMotion.Add((int)Motions.GoHand);
                BodyScript.effectTargetID.Add(-2);
            }
        }

        //update
        if (ManagerScript.getPhaseInt() == (int)Phases.UpPhase)
            onceTurn = false;

        field = ManagerScript.getFieldInt(ID, player);
	}

    bool checkClass(int x, int target)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, target);
        return (c[0] == 3 && c[1] == 1);
    }
}

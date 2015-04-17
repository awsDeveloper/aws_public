using UnityEngine;
using System.Collections;

public class WX05_042 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool bursted = false;
    bool cFlag = false;

    bool chantedFlag = false;
    int count = 0;
    int effectPhase = 0;

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
        countUpdate();

        //chant
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            BodyScript.setEffect(0, player, Motions.AntiCheck);
            BodyScript.AntiCheck = true;
            cFlag = true;
        }

        //anti check
        if (cFlag && BodyScript.effectTargetID.Count == 0)
        {
            cFlag = false;

            chantedFlag = true;
        }

        if (effectPhase == 1 && BodyScript.effectTargetID.Count == 0)
            effect_1();

        if (effectPhase == 2 && BodyScript.effectTargetID.Count == 0)
        {
            effectPhase = 0;
            BodyScript.setEffect(0, player, Motions.Draw);
        }


        //burst
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && BodyScript.BurstFlag)
        {
            BodyScript.effectFlag = true;
            BodyScript.effectTargetID.Add(50 * player);
            BodyScript.effectMotion.Add((int)Motions.TopEnaCharge);

            bursted = true;
        }

        //after burst
        if (bursted && BodyScript.effectTargetID.Count == 0)
        {
            bursted = false;

            if (ManagerScript.getFieldAllNum((int)Fields.ENAZONE, player) <= 4)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(50 * player);
                BodyScript.effectMotion.Add((int)Motions.TopEnaCharge);
            }
        }

        //UpDate
        field = ManagerScript.getFieldInt(ID, player);
    }

    void countUpdate()
    {
        if (ManagerScript.getStartedPhase() == (int)Phases.UpPhase)
        {
            chantedFlag = false;
            count = 0;
        }

        if (ManagerScript.getPhaseInt() != (int)Phases.MainPhase)
            return;

        int x = ManagerScript.DownID;
        if (x < 0) return;

        int target = x / 50;
        x = x % 50;

        if (!checkClass(x, target))
            return;

        count++;

        //効果発動のチェック
        if (!chantedFlag || count != 3) 
            return;

        chantedFlag = false;
        effect_0();
    }

    bool checkClass(int x, int cplayer)
    {
        if (x < 0) return false;
        int[] c = ManagerScript.getCardClass(x, cplayer);
        return (c[0] == 2 && c[1] == 2);
    }

    void effect_0()
    {
        int target = 1-player;
        int f = 3;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if (x >= 0)
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count > 0)
            BodyScript.setEffect(-1, 0, Motions.EnaCharge);

        effectPhase = 1;
    }

    void effect_1()
    {
        int target = player;
        int f = (int)Fields.ENAZONE;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);

            if (x >= 0)
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count > 0)
        {
            BodyScript.setEffect(-1, 0, Motions.GoHand);
            BodyScript.setEffect(0, target, Motions.EnaSort);
        }

        effectPhase = 2;
    }
}

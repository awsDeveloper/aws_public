using UnityEngine;
using System.Collections;

public class WX06_024 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool bursted = false;

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
        beforeChant();

        chant();

        burst();

        afterBurst();
	
	}

    void targetIN()
    {
        BodyScript.Targetable.Clear();
        BodyScript.cursorCancel = false;

        int target = 1-player;
        int f = (int)Fields.SIGNIZONE;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0)
                BodyScript.Targetable.Add(x + 50 * target);
        }
    }

    void beforeChant()
    {
        if (!BodyScript.chantEffectFlag)
            return;

        BodyScript.chantEffectFlag = false;

        int target = player;
        int f = (int)Fields.SIGNIZONE;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0 && ManagerScript.checkClass(x, target, cardClassInfo.精像_天使))
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.cursorCancel = true;
        BodyScript.addParameta(parametaKey.CostDownColor, (int)cardColorInfo.無色);
        BodyScript.addParameta(parametaKey.CostDownNum, 2);
        BodyScript.addParameta(parametaKey.SpellOrArts, 0);

        for (int i = 0; i < BodyScript.Targetable.Count; i++)
            BodyScript.setEffect(-1, 0, Motions.DownAndCostDown);
    }

    void chant()
    {
        if (!BodyScript.isChanted())
            return;

        targetIN();

        if (BodyScript.Targetable.Count == 0)
            return;

        BodyScript.setEffect(-1, 0, Motions.GoTrash);
    }

    void burst()
    {
        if (!BodyScript.isBursted())
            return;

        bursted = true;
        BodyScript.setAntiCheck();

    }

    void afterBurst()
    {
        if (!bursted || BodyScript.effectTargetID.Count > 0)
            return;

        bursted = false;

        if (BodyScript.AntiCheck)
        {
            BodyScript.AntiCheck = false;
            return;
        }
            

        targetIN();

        if (BodyScript.Targetable.Count == 0)
            return;

        if (BodyScript.isClassSigniOnBattleField(player, cardClassInfo.精像_天使))
            BodyScript.setEffect(-1, 0, Motions.GoTrash);
        else
            BodyScript.setEffect(-1, 0, Motions.EffectDown);
    }
}

using UnityEngine;
using System.Collections;

public class WX06_025 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool afterChantFlag = false;

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
        chant();

        afterChant();
    }

    void chant()
    {
        if (!BodyScript.isChanted())
            return;

        BodyScript.setAntiCheck();
        afterChantFlag = true;
    }

    void afterChant()
    {
        if (!afterChantFlag || BodyScript.effectTargetID.Count > 0)
            return;

        afterChantFlag = false;

        if (BodyScript.AntiCheck)
        {
            BodyScript.AntiCheck = false;
            return;
        }

        int target = player;
        int f = (int)Fields.MAINDECK;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0 && !ManagerScript.checkColor(x,target, cardColorInfo.無色) && ManagerScript.checkType(x,target, cardTypeInfo.シグニ))
                BodyScript.Targetable.Add(x + 50 * target);
        }

        if (BodyScript.Targetable.Count == 0)
            return;

        if (BodyScript.getTargLevNum() < 3)
        {
            BodyScript.Targetable.Clear();
            return;
        }

        BodyScript.targetableSameLevelRemove = true;
        BodyScript.setEffect(-2, 0, Motions.GoHand);
        BodyScript.setEffect(-2, 0, Motions.GoHand);
        BodyScript.setEffect(-2, 0, Motions.GoHand);
    }
}

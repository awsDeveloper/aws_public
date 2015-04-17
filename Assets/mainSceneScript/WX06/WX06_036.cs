using UnityEngine;
using System.Collections;

public class WX06_036 : MonoBehaviour {
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
        //cip
        if (BodyScript.isCiped())
        {
            targetIN();

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.Targetable.Clear();
                BodyScript.DialogFlag = true;
            }
        }

        //receive
        if (BodyScript.messages.Count > 0)
        {
            if (BodyScript.messages[0].Contains("Yes"))
            {

                targetIN();

                if (BodyScript.Targetable.Count > 0)
                {
                    costFlag = true;
                    BodyScript.effectFlag = true;
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add((int)Motions.GOLrigTrash);
                }
            }

            BodyScript.messages.Clear();
        }


        //after cost
        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            costFlag = false;
            BodyScript.Targetable.Clear();

            int target = player;
            int f = (int)Fields.TRASH;
            int num = ManagerScript.getNumForCard(f, target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0 && ManagerScript.checkClass(x,target, cardClassInfo.精械_古代兵器))
                    BodyScript.Targetable.Add(x + 50 * target);
            }

            if (BodyScript.Targetable.Count > 0)
                BodyScript.setEffect(-2, 0, Motions.Summon);
        }
     }

    void targetIN()
    {
        int target = player;
        int f = (int)Fields.LRIGDECK;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x >= 0 && ManagerScript.checkType(x,target, cardTypeInfo.アーツ) && ManagerScript.checkColor(x,target, cardColorInfo.黒))
                BodyScript.Targetable.Add(x + 50 * target);
        }
    }
}
using UnityEngine;
using System.Collections;

/*public class WX05_007 : MonoBehaviour {
   GameObject Manager;
    DeckScript ManagerScript;
    GameObject Body;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    int field = -1;

    bool costFlag = false;
    // Use this for initialization
    void Start()
    {
        Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();

        BodyScript.attackArts = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (ManagerScript.getFieldInt(ID, player) == 8 && field != 8 && !BodyScript.BurstFlag)
        {
            int target = player;
            int f = (int)Fields.LRIGZONE;
            int num=ManagerScript.getNumForCard(f,target);

            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0 && x!=ManagerScript.getLrigID(target))
                    BodyScript.Targetable.Add(x + 50 * (target));
            }

            if (BodyScript.Targetable.Count >= 3)
            {
                costFlag = true;
                BodyScript.effectFlag = true;
                for (int i = 0; i < 3; i++)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add((int)Motions.GOLrigTrash);
                }
            }
            else
                BodyScript.Targetable.Clear();
        }

        //after cost
        if (costFlag && BodyScript.effectTargetID.Count == 0)
        {
            BodyScript.Targetable.Clear();
            costFlag = false;

            if (ManagerScript.getLastMotionsRear() == (int)Motions.GOLrigTrash)
            {
                int target = 1 - player;
                for (int i = 0; i < 3; i++)
                {
                    int x = ManagerScript.getFieldRankID(3, i, target);
                    if (x >= 0)
                    {
                        BodyScript.Targetable.Add(x + 50 * (target));
                    }
                }
                if (BodyScript.Targetable.Count > 0)
                {
                    BodyScript.effectFlag = true;
                    BodyScript.effectTargetID.Add(-1);
                    BodyScript.effectMotion.Add((int)Motions.GoTrash);
                }
            }
        }

        field = ManagerScript.getFieldInt(ID, player);
    }
}*/
public class WX05_007 : MonoCard
{

    // Use this for initialization
    void Start()
    {
        sc.attackArts = true;

        var c = sc.AddEffectTemplete(EffectTemplete.triggerType.Chant);
        c.addEffect(chant, EffectTemplete.option.ifThen);
        c.addEffect(chant_1);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void chant()
    {
        ms.targetableExceedIn(player, sc);

        if (sc.Targetable.Count < 4)
        {
            sc.Targetable.Clear();
            return;
        }

        for (int i = 0; i < 4; i++)
            sc.setEffect(-2, 0, Motions.GoTrash);
    }

    void chant_1()
    {
        sc.setFuncEffect(-1, Motions.GoTrash, 1 - player, Fields.SIGNIZONE, null);
    }
}


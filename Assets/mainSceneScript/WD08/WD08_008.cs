using UnityEngine;
using System.Collections;

public class WD08_008 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool afterChantFlag = false;
    cardClassInfo info;

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

        BodyScript.setEffect(0, player, Motions.TopGoTrash);
        BodyScript.setEffect(0, player, Motions.TopGoTrash);
        BodyScript.setEffect(0, player, Motions.TopGoTrash);
        BodyScript.setEffect(0, player, Motions.TopGoTrash);
        BodyScript.setEffect(0, player, Motions.TopGoTrash);

        afterChantFlag = true;
    }

    void afterChant()
    {
        if (!afterChantFlag || BodyScript.effectTargetID.Count > 0)
            return;

        afterChantFlag = false;

        int index = 0;
        int count = 0;

        foreach (var item in System.Enum.GetValues(typeof(cardClassInfo)))
        {
            count = 0;
            index = 0;

            while (true)
            {
                int x = ManagerScript.getTopGoTrashListID(index, ID, player);

                if (x == -1)
                    break;

                if (ManagerScript.checkClass(x, (cardClassInfo)item))
                    count++;

                index++;
            }


            if (count >= 3)
            {
                info = (cardClassInfo)item;

                BodyScript.funcTargetIn(player, Fields.TRASH, checkInfo);

                for (int i = 0; i < 2 && i < BodyScript.Targetable.Count; i++)
                    BodyScript.setEffect(-2, 0, Motions.GoHand);

                break;
            }
        }
    }

    bool checkInfo(int x, int target)
    {
        return ManagerScript.checkClass(x, target, info);
    }
}

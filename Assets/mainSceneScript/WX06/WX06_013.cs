using UnityEngine;
using System.Collections;

public class WX06_013 : MonoBehaviour
{
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;
    bool afterEffect = false;
    bool afterGoHand = false;

    int count = -1;
    bool returnShowZone = false;

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
        if (ManagerScript.getCipID() == ID + 50 * player)
        {
            for (int i = 0; i < 2; i++)
                BodyScript.setEffect(0, player, Motions.TopGoShowZone);
            afterEffect = true;
        }

        //after effect
        if (afterEffect && BodyScript.effectTargetID.Count == 0)
        {
            afterEffect = false;
            afterGoHand = true;

            int index = 0;
            while (true)
            {
                int x = ManagerScript.getShowZoneID(index);

                if (x < 0)
                    break;

                if (ManagerScript.checkClass(x % 50, x / 50, cardClassInfo.精像_美巧))
                    BodyScript.Targetable.Add(x);

                index++;
            }

            if (BodyScript.Targetable.Count > 0)
                BodyScript.setEffect(-1, 0, Motions.DontShuffleGoHand);
        }

        //after go hand
        if (afterGoHand && BodyScript.effectTargetID.Count == 0)
        {
            afterGoHand = false;

            BodyScript.DialogFlag = true;
            BodyScript.DialogNum = 4;
         }


        //receive
        if (BodyScript.messages.Count > 0)
        {
            count = int.Parse(BodyScript.messages[0]);
            returnShowZone = true;

            BodyScript.messages.Clear();
        }

        if (returnShowZone && BodyScript.effectTargetID.Count == 0)
        {
            Motions m = Motions.GoDeck;
            if (count == 1)
                m = Motions.GoDeckBottom;

            int index = 0;
            while (true)
            {
                int x = ManagerScript.getShowZoneID(index);

                if (x < 0)
                    break;

                BodyScript.Targetable.Add(x);
                index++;
            }

            if (BodyScript.Targetable.Count == 1)
            {
                BodyScript.setEffect(-1, 0, m);
                returnShowZone = false;
            }
            else if (BodyScript.Targetable.Count >= 2)
            {
                BodyScript.setEffect(-1, 0, m);
            }
        }
    }
}

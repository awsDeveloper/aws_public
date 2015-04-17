using UnityEngine;
using System.Collections;

public class PR_111 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    bool afterEffect = false;

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
            BodyScript.setEffect(0, player, Motions.TopGoShowZone);
            afterEffect = true;
        }

        //after effect
        if (afterEffect && BodyScript.effectTargetID.Count == 0)
        {
            afterEffect = false;

            int index = 0;
            bool flag = false;
            while (true)
            {
                int x = ManagerScript.getShowZoneID(index);

                if (x < 0)
                    break;

                if (ManagerScript.checkColor(x % 50, x / 50, cardColorInfo.赤))
                    flag = true;

                BodyScript.Targetable.Add(x);

                index++;
            }

            if (!flag)
                BodyScript.setEffect(ID, player, Motions.GoTrash);

            if (BodyScript.Targetable.Count > 0)
            {
                for (int i = 0; i < BodyScript.Targetable.Count; i++)
                {
                    BodyScript.effectTargetID.Add(-2);
                    BodyScript.effectMotion.Add((int)Motions.GoDeck);
                }
            }
        }

        //burst
        if (BodyScript.isBursted())
            BodyScript.setEffect(0, player, Motions.Draw);
    }
}

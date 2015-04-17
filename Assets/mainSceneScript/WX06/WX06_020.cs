using UnityEngine;
using System.Collections;

public class WX06_020 : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    // Use this for initialization
    void Start()
    {
        GameObject Body = transform.parent.gameObject;
        BodyScript = Body.GetComponent<CardScript>();
        ID = BodyScript.ID;
        player = BodyScript.player;

        GameObject Manager = Body.GetComponent<CardScript>().Manager;
        ManagerScript = Manager.GetComponent<DeckScript>();
        
        gameObject.AddComponent<YourSigniCountUp>();
    }

    // Update is called once per frame
    void Update()
    {
        cip();

        burst();
    }
    void cip()
    {
        if (!BodyScript.isCiped())
            return;

        int target = player;
        int f = (int)Fields.SIGNIZONE;
        int num = ManagerScript.getNumForCard(f, target);

        for (int i = 0; i < num; i++)
        {
            int x = ManagerScript.getFieldRankID(f, i, target);
            if (x != ID && ManagerScript.checkClass(x, target, cardClassInfo.精羅_植物) && ManagerScript.checkBurstIcon(x, target))
                BodyScript.setEffect(0, player, Motions.TopEnaCharge);
        }
    }

    void burst()
    {
        if (!BodyScript.isBursted())
            return;

        BodyScript.setEffect(0, player, Motions.TopEnaCharge);
        BodyScript.setEffect(0, player, Motions.TopEnaCharge);
    }
}

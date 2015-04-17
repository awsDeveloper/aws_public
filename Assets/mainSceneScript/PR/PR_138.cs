using UnityEngine;
using System.Collections;

public class PR_138 : MonoBehaviour {
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
            BodyScript.setEffect(0, player, Motions.TopGoShowZone);
            BodyScript.setEffect(0, player, Motions.TopGoShowZone);
            afterEffect = true;
        }

        //after effect
        if (afterEffect && BodyScript.effectTargetID.Count == 0)
        {
            afterEffect = false;

            BodyScript.showZoneTargIn(check);

            if(BodyScript.Targetable.Count==3)
                BodyScript.setEffect(-1, 0, Motions.GoHand);
            BodyScript.setEffect(-1, 0, Motions.ShowZoneGoTop);
        }


        //burst
        if (BodyScript.isBursted())
            BodyScript.setEffect(0, player, Motions.Draw);
    }

    bool check(int x, int target)
    {
        return ManagerScript.checkClass(x, target, cardClassInfo.精像_天使);
    }
}
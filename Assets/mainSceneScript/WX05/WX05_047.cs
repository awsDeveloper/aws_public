using UnityEngine;
using System.Collections;

public class WX05_047 : MonoBehaviour {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (ManagerScript.getBattledID(player) == ID)
        {
            int bID = ManagerScript.getBattledID(1-player);

            if (ManagerScript.getCardLevel(bID, 1 - player) != 4)
                return;

                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(bID + 50 * (1 - player));
                BodyScript.effectMotion.Add(5);
        }
    }
}

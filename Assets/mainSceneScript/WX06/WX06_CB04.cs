using UnityEngine;
using System.Collections;

public class WX06_CB04 : MonoBehaviour {
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
	void Update () {
	
        if(ManagerScript.getBanishedID() == ID+50*player)
        {
            BodyScript.funcTargetIn(1-player, Fields.SIGNIZONE,tarCheck);

            if(BodyScript.Targetable.Count==0)
                return;

            BodyScript.powerUpValue=-2000;
            BodyScript.setEffect(-1,0, Motions.PowerUpEndPhase);
        }

        burst();
	}

    void burst()
    {
        if (!BodyScript.isBursted())
            return;

        BodyScript.setEffect(0, player, Motions.Draw);
    }

    bool tarCheck(int x,int target)
    {
        return true;
    }
}

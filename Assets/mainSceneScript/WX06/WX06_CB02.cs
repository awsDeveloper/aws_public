using UnityEngine;
using System.Collections;

public class WX06_CB02 : MonoBehaviour {
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


        FuncChangeBase fcb = gameObject.AddComponent<FuncChangeBase>();
        fcb.setFunc(check);
        fcb.baseValue = 10000;
    }	

	// Update is called once per frame
	void Update () {
        //cip
        if (BodyScript.isCiped() && ManagerScript.getFieldAllNum((int)Fields.HAND, 1 - player) > 0)
        {
            BodyScript.setEffect(0, 1 - player, Motions.OpenHand);

            BodyScript.funcTargetIn(1 - player, Fields.HAND, targetCheck);
            BodyScript.setEffect(-1, 0, Motions.AntiCheck);

            BodyScript.setEffect(0, 1 - player, Motions.CloseHand);
        }

        //burst
        if (BodyScript.isBursted() && ManagerScript.getFieldAllNum((int)Fields.HAND, 1 - player) > 0)
        {
            BodyScript.setEffect(0, 1 - player, Motions.OpenHand);

            BodyScript.funcTargetIn(1 - player, Fields.HAND, targetCheck);
            BodyScript.setEffect(-1, 0, Motions.HandDeath);

            BodyScript.setEffect(0, 1 - player, Motions.CloseHand);
        }	
	}

    bool check()
    {
        return ManagerScript.getFieldAllNum((int)Fields.HAND, 1 - player) == 0;
    }

    bool targetCheck(int x, int target)
    {
        return true;
    }
}

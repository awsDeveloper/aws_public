using UnityEngine;
using System.Collections;

public class WD08_007 : MonoBehaviour {
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
        chant();
	
	}

    void chant()
    {
        if (!BodyScript.isChanted())
            return;

        BodyScript.funcTargetIn(player, Fields.TRASH, chantCheck);

        BodyScript.setEffect(-2, 0, Motions.Summon);
    }

    bool chantCheck(int x, int target)
    {
        return ManagerScript.checkClass(x, target, cardClassInfo.精械_古代兵器);
    }
}

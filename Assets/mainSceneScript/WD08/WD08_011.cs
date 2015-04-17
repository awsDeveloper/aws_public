using UnityEngine;
using System.Collections;

public class WD08_011 : MonoBehaviour {
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
        cip();
	
	}

    void cip()
    {
        if (!BodyScript.isCiped())
            return;

        BodyScript.funcTargetIn(player, Fields.TRASH, cipCheck);

        if (BodyScript.getTargLevNum() < 4)
            return;

        BodyScript.targetableSameLevelRemove = true;
        BodyScript.setEffect(-2, 0, Motions.GoDeckBottom);
        BodyScript.setEffect(-2, 0, Motions.GoDeckBottom);
        BodyScript.setEffect(-2, 0, Motions.GoDeckBottom);
        BodyScript.setEffect(-2, 0, Motions.GoDeckBottom);

        BodyScript.setEffect(0, player, Motions.Draw);
    }

    bool cipCheck(int x,int target)
    {
        return ManagerScript.checkClass(x, target, cardClassInfo.精械_古代兵器);
    }
}

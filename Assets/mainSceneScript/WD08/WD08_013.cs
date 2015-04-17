using UnityEngine;
using System.Collections;

public class WD08_013 : MonoBehaviour {
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

        FuncChangeBase fcb = gameObject.AddComponent<FuncChangeBase>();
        fcb.baseValue = 8000;
        fcb.setFunc(check);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    bool check()
    {
        return ManagerScript.getClassNum(player, Fields.TRASH, cardClassInfo.精械_古代兵器) >= 3;
    }
}

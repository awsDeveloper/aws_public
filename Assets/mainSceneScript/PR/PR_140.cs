using UnityEngine;
using System.Collections;

public class PR_140 : MonoBehaviour {
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
        if (BodyScript.isCiped() && BodyScript.getFreezeNum(1 - player) > 0)
            BodyScript.setEffect(0, player, Motions.Draw);
	}
}

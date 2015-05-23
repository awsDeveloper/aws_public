using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class oneceTurnList : MonoBehaviour {
    DeckScript ManagerScript;
    CardScript BodyScript;
    int ID = -1;
    int player = -1;

    List<bool> onceTurns = new List<bool>();

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
        if (ManagerScript.getStartedPhase() != (int)Phases.UpPhase)
            return;

        for (int i = 0; i < onceTurns.Count; i++)
            onceTurns[i] = false;	
	}

    public bool onceIsFalse(int index)
    {
        if (onceTurns.Count <= index)
            return false;
        return !onceTurns[index];
    }

    public void onceUp(int index)
    {
        if (onceTurns.Count <= index)
            return;

        onceTurns[index] = true;        
    }

    public void addOnce()
    {
        onceTurns.Add(false);
    }
}

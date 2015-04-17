using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IgniAdd : MonoBehaviour {
    public bool Ignition = false;
    public bool upIgnition = false;

    public List<int> IgniList = new List<int>();

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
        if (Ignition)
        {
            upIgnition = true;

            Ignition = false;
            return;
        }

        if(upIgnition)
        {
            upIgnition = false;

            BodyScript.Targetable.Add(ID + player * 50);

            while (IgniList.Count > 0)
            {
                BodyScript.Targetable.Add(IgniList[0]);
                IgniList.RemoveAt(0);
            }

            if (BodyScript.Targetable.Count > 0)
            {
                BodyScript.effectFlag = true;
                BodyScript.effectTargetID.Add(-2);
                BodyScript.effectMotion.Add(72);
            }
        }
	}

    public void setIgniTarget(int ID , int player)
    {
        if (!IgniList.Contains(ID + 50 * player))
            IgniList.Add(ID + 50 * player);
    }

}

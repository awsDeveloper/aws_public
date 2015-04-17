﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WX01_019sc : MonoBehaviour {
	DeckScript ManagerScript;
	CardScript BodyScript;
	int ID=-1;
	int player=-1;

	// Use this for initialization
	void Start () {
		GameObject Body=transform.parent.gameObject;
		BodyScript=Body.GetComponent<CardScript>();
		ID=BodyScript.ID;
		player=BodyScript.player;
		
		GameObject Manager=Body.GetComponent<CardScript>().Manager;
		ManagerScript=Manager.GetComponent<DeckScript>();

        BodyScript.powerUpValue = 1000;
	}
	
	// Update is called once per frame
	void Update () {
        //up requirement
        if (ManagerScript.getLrigID(player) == ID )
        {
            int target = player;
            int f = 3;
            int num = ManagerScript.getNumForCard(f, target);

            //check exist in upList
            for (int i = 0; i < num; i++)
            {
                int x = ManagerScript.getFieldRankID(f, i, target);
                if (x >= 0 && !ManagerScript.checkChanListExist(x, target, ID, player))
                {
                    //requirement add upList
                    if (true)
                        ManagerScript.alwaysChagePower(x, target, BodyScript.powerUpValue, ID, player);
                }
            }
        }
        else
            ManagerScript.powChanListChangerClear(ID, player);
	}
}

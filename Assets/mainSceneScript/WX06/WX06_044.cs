﻿using UnityEngine;
using System.Collections;

public class WX06_044 : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        gameObject.AddComponent<classBase>();
        classBase cb = gameObject.GetComponent<classBase>();

        cb.setClass(cardClassInfo.精械_電機);
        cb.baseValue = 12000;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
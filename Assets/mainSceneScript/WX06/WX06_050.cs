﻿using UnityEngine;
using System.Collections;

public class WX06_050 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<classBase>();
        classBase cb = gameObject.GetComponent<classBase>();

        cb.setClass(cardClassInfo.精像_悪魔);
        cb.baseValue = 12000;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

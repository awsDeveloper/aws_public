﻿using UnityEngine;
using System.Collections;

public class WX06_043 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<classBase>();
        classBase cb = gameObject.GetComponent<classBase>();

        cb.setClass(cardClassInfo.精羅_鉱石);
        cb.setClass(cardClassInfo.精羅_宝石);
        cb.baseValue = 5000;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
﻿using UnityEngine;
using System.Collections;

public class WX06_039 : MonoBehaviour {

	// Use this for initialization
	void Start () {

        gameObject.AddComponent<classBase>();
        classBase cb = gameObject.GetComponent<classBase>();

        cb.setClass(cardClassInfo.精武_アーム);
        cb.baseValue = 8000;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

﻿using UnityEngine;
using System.Collections;

public class WX07_060 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
            sc.maxPowerBanish(2000);
	
	}
}


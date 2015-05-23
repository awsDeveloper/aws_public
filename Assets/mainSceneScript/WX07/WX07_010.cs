using UnityEngine;
using System.Collections;

public class WX07_010 :MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();

        var com = gameObject.AddComponent<FuncPowerUp>();
        com.setTrueTrigger(1000, ms.checkHavingCross);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


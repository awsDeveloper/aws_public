using UnityEngine;
using System.Collections;

public class WX08_048 : MonoCard {

	// Use this for initialization
	void Start () {
        var com = gameObject.AddComponent<classBase>();
        com.setClass(cardClassInfo.精羅_宇宙);
        com.baseValue = 12000;

	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


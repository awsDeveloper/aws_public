using UnityEngine;
using System.Collections;

public class WX06_047 : MonoBehaviour {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<classBase>();
        classBase cb = gameObject.GetComponent<classBase>();

        cb.setClass(cardClassInfo.精生_空獣);
        cb.setClass(cardClassInfo.精生_地獣);
        cb.baseValue = 12000;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

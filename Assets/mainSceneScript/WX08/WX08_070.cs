using UnityEngine;
using System.Collections;

public class WX08_070 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<ThreeClassBase>().set(cardClassInfo.精像_美巧, 6000);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


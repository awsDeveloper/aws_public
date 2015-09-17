using UnityEngine;
using System.Collections;

public class WX08_063 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<ThreeClassBase>().set(cardClassInfo.精械_電機, 9000);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


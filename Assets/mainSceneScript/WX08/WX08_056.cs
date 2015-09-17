using UnityEngine;
using System.Collections;

public class WX08_056 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<ThreeClassBase>().set(cardClassInfo.鉱石または宝石, 9000);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


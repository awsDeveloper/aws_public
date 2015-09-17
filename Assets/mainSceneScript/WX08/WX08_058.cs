using UnityEngine;
using System.Collections;

public class WX08_058 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<ThreeClassBase>().set(cardClassInfo.鉱石または宝石, 6000);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


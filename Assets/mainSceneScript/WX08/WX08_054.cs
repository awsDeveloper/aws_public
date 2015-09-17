using UnityEngine;
using System.Collections;

public class WX08_054 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<ThreeClassBase>().set(cardClassInfo.鉱石または宝石, 14000);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


using UnityEngine;
using System.Collections;

public class WX07_053 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<BanishedSearch>().set(new colorCostArry(cardColorInfo.白, 1), "羅星　アクトゥルス");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


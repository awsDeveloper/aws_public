using UnityEngine;
using System.Collections;

public class WX07_040 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.CrossingCip())
            sc.setEffect(0, player, Motions.Draw);
	
	}
}


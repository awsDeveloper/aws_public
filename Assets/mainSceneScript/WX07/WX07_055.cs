using UnityEngine;
using System.Collections;

public class WX07_055 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.GoHand);
            sc.setEffect(-1, 0, Motions.GoHand);
            sc.GUIcancelEnable = true;
        }
	
	}
}


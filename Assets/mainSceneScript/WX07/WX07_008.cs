using UnityEngine;
using System.Collections;

public class WX07_008 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.attackArts = true;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.LostEffectEnd);
        }
	
	}
}


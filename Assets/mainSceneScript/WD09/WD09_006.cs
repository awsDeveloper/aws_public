using UnityEngine;
using System.Collections;

public class WD09_006 : MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();

        sc.attackArts = true;
        sc.notMainArts = true;	
	}
	
	// Update is called once per frame
	void Update () {

        if (!sc.isChanted())
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.DontAttack);

        sc.setEffect(0, player, Motions.oneHandDeath);	
	}
}


using UnityEngine;
using System.Collections;

public class SP03_010 : MonoCard{

	// Use this for initialization
	void Start ()
	{
		beforeStart();

        sc.attackArts = true;
	}

	// Update is called once per frame
	void Update ()
	{
        if (!sc.isChanted())
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
        sc.powerUpValue = -2000;
	}
}

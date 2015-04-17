using UnityEngine;
using System.Collections;

public class SP07_010 : MonoCard{

	// Use this for initialization
	void Start ()
	{
		beforeStart();

	}

	// Update is called once per frame
	void Update ()
	{
        if (!sc.isChanted())
            return;

        sc.setEffect(0, 1 - player, Motions.MaxPowerBounce);

	}
}

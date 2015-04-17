using UnityEngine;
using System.Collections;

public class SP06_010 : MonoCard{

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

        sc.setEffect(0, player, Motions.Draw);
        sc.setEffect(0, player, Motions.Draw);
        sc.setEffect(0, player, Motions.Draw);
        sc.setEffect(0, player, Motions.oneHandDeath);
        sc.setEffect(0, player, Motions.oneHandDeath);
	}
}

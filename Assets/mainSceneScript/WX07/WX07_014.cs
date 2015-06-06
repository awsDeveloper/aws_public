using UnityEngine;
using System.Collections;

public class WX07_014 : MonoCard {

	// Use this for initialization
	void Start () {

        sc.SpellCutIn = true;
        sc.notMainArts = true;
	}
	
	// Update is called once per frame
	void Update () {
        sc.useLimit = !sc.isCrossOnBattleField(player);

        if (sc.isChanted())
            sc.setEffect(0, player, Motions.CounterSpell);
	
	}
}


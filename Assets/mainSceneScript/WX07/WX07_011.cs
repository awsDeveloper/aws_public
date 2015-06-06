using UnityEngine;
using System.Collections;

public class WX07_011 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.funcTargetIn(player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.EnAbility);
            sc.addParameta(parametaKey.EnAbilityType, (int)ability.assassin);
        }
	}
}


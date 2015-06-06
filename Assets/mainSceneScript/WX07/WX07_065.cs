using UnityEngine;
using System.Collections;

public class WX07_065 : MonoCard{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        sc.changeColorCost(cardColorInfo.青, 5 - sc.getFreezeNum(1 - player));

        chant();	
	}

    void chant()
    {
        if (!sc.isChanted())
            return;

        sc.setFieldAllEffect(player, Fields.SIGNIZONE, Motions.EnAbility);
        sc.addParameta(parametaKey.EnAbilityType, (int)ability.FreezeThrough);
    }
}


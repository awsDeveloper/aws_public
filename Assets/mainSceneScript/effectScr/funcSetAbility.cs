using UnityEngine;
using System.Collections;

public class funcSetAbility : MonoCard {

    System.Func<bool> func;
    ability myAbility = ability.Lanaje;
    bool upFlag=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (func == null)
           return;

        if (sc.isOnBattleField() && func())
        {
            sc.setAbilitySelf(myAbility, true);
            upFlag = true;
        }
        else if (upFlag)
        {
            upFlag = false;
            sc.setAbilitySelf(myAbility, false);
        } 	
	}

    public void set(System.Func<bool> _func, ability _ability)
    {
        func = _func;
        myAbility = _ability;
    }
}


using UnityEngine;
using System.Collections;

public class PR_213 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<FuncChangeBase>().set(tri, 5000);
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.setAbilitySelf(ability.LrigColorEna,true);
	
	}

    bool che(int x, int target)
    {
        return !ms.checkColor(x, target, cardColorInfo.白);
    }

    bool tri()
    {
        return sc.isFuncOnBatteField(che, player);
    }
}


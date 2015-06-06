using UnityEngine;
using System.Collections;

public class WX07_007 : MonoCard {

	// Use this for initialization
	void Start () {
        var com = gameObject.AddComponent<FuncPowerUp>();
        com.set(2000, sc.isOnBattleField, check);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool check(int x, int target)
    {
        return ms.checkType(x, target, cardTypeInfo.レゾナ);
    }
}


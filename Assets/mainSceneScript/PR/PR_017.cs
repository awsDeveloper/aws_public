using UnityEngine;
using System.Collections;

public class PR_017 : MonoCard {
	// Use this for initialization
	void Start () {
        gameObject.AddComponent<FuncChangeBase>().set(check, 10000);
	}
	
	// Update is called once per frame
	void Update () {
	}

    bool check()
    {
        return ms.getLrigLevel(player) == 4 && ms.checkLrigType(player, LrigTypeInfo.タマ);
    }
}

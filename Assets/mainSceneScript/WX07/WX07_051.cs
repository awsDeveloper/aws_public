using UnityEngine;
using System.Collections;

public class WX07_051 : MonoCard {

	// Use this for initialization
	void Start () {
        var com = gameObject.AddComponent<FuncPowerUp>();
        com.set(2000, tri);
        com.setIsSelfUp();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool tri()
    {
        return ms.getClassNum(player, Fields.SIGNIZONE, cardClassInfo.精羅_宇宙) >= 3;
    }

}


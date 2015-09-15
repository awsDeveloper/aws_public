using UnityEngine;
using System.Collections;

public class WX08_033 : MonoCard {

	// Use this for initialization
	void Start () {
        var com = gameObject.AddComponent<FuncChangeBase>();
        com.baseValue = 18000;
        com.setFunc(sc.isResonaOnBattleField);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}


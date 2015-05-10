using UnityEngine;
using System.Collections;

public class WD10_013 : MonoCard {

	// Use this for initialization
	void Start () {
        beforeStart();

        var com = gameObject.AddComponent<FuncChangeBase>();
        com.baseValue = 8000;
        com.setFunc(sc.isCrossing);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


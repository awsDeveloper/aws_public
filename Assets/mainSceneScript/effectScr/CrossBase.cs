using UnityEngine;
using System.Collections;

public class CrossBase :MonoCard {
    public int upBase=5000;
    public string CrossRightName = "";
    public string CrossLeftName = "";

    // Use this for initialization
	void Start () {
        beforeStart();

        var com = gameObject.AddComponent<FuncChangeBase>();
        com.baseValue = upBase;
        com.setFunc(sc.isCrossing);

        if (CrossLeftName != "")
            sc.CrossLeftName = CrossLeftName;

        if (CrossRightName != "")
            sc.CrossRightName = CrossRightName;
        
	}	
}


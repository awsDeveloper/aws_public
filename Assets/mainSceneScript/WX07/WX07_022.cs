using UnityEngine;
using System.Collections;

public class WX07_022 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isChanted())
        {
            sc.funcTargetIn(1 - player, Fields.TRASH);

            for (int i = 0; i < 3; i++)
                sc.setEffect(-2, 0, Motions.Exclude);

            sc.GUIcancelEnable = true;
        }
	}
}


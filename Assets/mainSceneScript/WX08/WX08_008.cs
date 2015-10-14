using UnityEngine;
using System.Collections;

public class WX08_008 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.resonaSummon(sc.notResonaAndUtyu, 2);

        if (sc.isCiped())
        {
            sc.funcTargetIn(player, Fields.MAINDECK, new checkFuncs(ms, cardTypeInfo.シグニ).check);
            sc.setEffect(-2, 0, Motions.Summon);
        }
	}
}


using UnityEngine;
using System.Collections;

public class WX08_015 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.useLimit = ms.getFieldAllNum((int)Fields.LIFECLOTH, player) != 0;

        if (sc.isChanted() && !ms.isUsedThis(ID, player))
        {
            sc.setEffect(0, player, Motions.TopGoLifeCloth);
            sc.setEffect(0, player, Motions.TopGoLifeCloth);
        }
	}
}


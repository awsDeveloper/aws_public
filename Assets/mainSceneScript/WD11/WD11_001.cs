using UnityEngine;
using System.Collections;

public class WD11_001 : MonoCard {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (sc.isMyResonaCiped())
            sc.setEffect(0, player, Motions.TopEnaCharge);


        if (sc.IgnitionDown())
        {
            sc.ClassTargetIn(player, Fields.TRASH, cardClassInfo.精生_凶蟲);
            sc.setEffect(-2, 0, Motions.GoHand);
        }
	}
}


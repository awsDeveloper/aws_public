using UnityEngine;
using System.Collections;

public class WX08_040 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.crossCip, cip);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void cip()
    {
        sc.setEffect(0, 1 - player, Motions.oneHandDeath);
    }
}


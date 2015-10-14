using UnityEngine;
using System.Collections;

public class PR_207 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void chant()
    {
        sc.setEffect(0, player, Motions.TopEnaCharge);
        sc.setEffect(0, player, Motions.TopEnaCharge);
        sc.setEffect(0, player, Motions.TopEnaCharge);
        sc.setEffect(0, player, Motions.CantUseSpellAndArts);
    }
}


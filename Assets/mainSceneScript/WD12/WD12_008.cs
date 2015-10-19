using UnityEngine;
using System.Collections;

public class WD12_008 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, sc.getEffects(CardEffectType.ThreeTopEnaCharge));
	
	}
	
	// Update is called once per frame
	void Update () {
        sc.resonaSummon(sc.notResonaAndYugu, 2);
	
	}
}


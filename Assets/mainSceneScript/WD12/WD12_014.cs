using UnityEngine;
using System.Collections;

public class WD12_014 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.ThisSigniYourSigniBanished, sc.getEffects(CardEffectType.ThisSigniGoHand), false, true);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, sc.getEffects(CardEffectType.OneDraw));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}


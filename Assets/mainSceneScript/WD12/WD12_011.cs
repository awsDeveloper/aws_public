using UnityEngine;
using System.Collections;

public class WD12_011 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.MySigniEffectSummon, false, true).addEffect(sc.getEffects(CardEffectType.OneDraw));

        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, sc.getEffects(CardEffectType.OneDraw));
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}


using UnityEngine;
using System.Collections;

public class WX08_038 : MonoCard {

	// Use this for initialization
	void Start () {
        var cb = gameObject.AddComponent<CrossBase>();
        cb.upBase = 8000;

        sc.AddEffectTemplete(EffectTemplete.triggerType.isHeavened, eff);
        sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, eff);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void eff()
    {
        sc.setEffect(0, player, Motions.Draw);
    }
}


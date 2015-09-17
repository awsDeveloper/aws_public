using UnityEngine;
using System.Collections;

public class WX08_078 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.removed, removed).addEffect(removed_2);
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    void removed()
    {
        sc.setFuncEffect(-2, Motions.SetCharmizeID, 1 - player, Fields.TRASH, null);
    }

    void removed_2() {
        sc.setFuncEffect(-1, Motions.SetCharm, 1 - player, Fields.SIGNIZONE, null);
    }
}


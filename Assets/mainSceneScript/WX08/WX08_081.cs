using UnityEngine;
using System.Collections;

public class WX08_081 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void chant()
    {
        sc.setFieldAllEffect(1 - player, Fields.SIGNIZONE, Motions.TopSetCharm);
    }
}


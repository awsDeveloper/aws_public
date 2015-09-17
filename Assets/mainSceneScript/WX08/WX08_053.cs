using UnityEngine;
using System.Collections;

public class WX08_053 : MonoCard {

	// Use this for initialization
	void Start () {
        var ch= sc.AddEffectTemplete(EffectTemplete.triggerType.Chant);
        ch.addEffect(chant, EffectTemplete.option.ifThen);
        ch.addEffect(chant_2);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool check(int x, int target)
    {
        return ms.checkType(x, target, cardTypeInfo.レゾナ);
    }

    void chant()
    {
        sc.setFuncEffect(-1, Motions.EffectDown, player, Fields.SIGNIZONE, check);
    }

    void chant_2(){
        sc.setFuncEffect(-1, Motions.GoTrash, 1-player, Fields.SIGNIZONE, null);

    }
}


using UnityEngine;
using System.Collections;

public class WD12_010 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.ThisSigniGoHandFromEnazone, triggered, false, true);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void triggered()
    {
        int lev=3;
        var cf=new checkFuncs(ms, lev,false);
        cf.setMax(lev);
        sc.setFuncEffect(-1, Motions.DownSummon, player, Fields.HAND, cf.check);
    }
}


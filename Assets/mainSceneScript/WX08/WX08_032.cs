using UnityEngine;
using System.Collections;

public class WX08_032 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemp_useTargetInput(EffectTemplete.triggerType.Burst, burst, burst_targetIn);

        sc.AddEffectTemplete(EffectTemplete.triggerType.Chant, chant);
	}
	
	// Update is called once per frame
    void Update()
    {
        sc.setCostDownValue(cardColorInfo.黒, getDownNum());
    }

    int getDownNum()
    {
        return ms.getClassNum(player, Fields.SIGNIZONE, cardClassInfo.精生_凶蟲) + ms.getCharmNum(1 - player);
    }


    void chant()
    {
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.WarmHole);
    }

    void burst_targetIn()
    {
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.targetInputEffect();
    }

    void burst()
    {
        int x = sc.TargetID[0];

        if (ms.havingCharm(x % 50, x / 50))
            sc.powerUpValue = -15000;
        else
            sc.powerUpValue = -8000;

        sc.setEffect(x, 0, Motions.PowerUpEndPhase);
    }
}


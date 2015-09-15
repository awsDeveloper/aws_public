using UnityEngine;
using System.Collections;

public class WX08_031 : MonoCard {
    FuncPowerUp fpu;

	// Use this for initialization
	void Start () {
        fpu = gameObject.AddComponent<FuncPowerUp>();
        fpu.setTrueTrigger(-2000, alwysCheck, 1-sc.player);

        var tri = sc.AddEffectTemplete(sc.isMyResonaCiped);
        tri.addEffect(trigger);

        var bu = sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst);
        bu.addEffect(burst_2);
	}
	
	// Update is called once per frame
	void Update () {
        fpu.setPUV(-2000 * ms.getCharmNum(1 - player));
	
	}

    bool alwysCheck(int x, int target)
    {
        return true;
    }

    void trigger()
    {
        sc.setFieldAllEffect(1 - player, Fields.SIGNIZONE, Motions.PowerUpEndPhase, ms.havingCharm);
        sc.powerUpValue = -5000;
    }


    void burst()
    {
        sc.ClassTargetIn(player, Fields.TRASH, cardClassInfo.精生_凶蟲);
        sc.setEffect(-2, 0, Motions.GoHand);
    }

    void burst_2()
    {
        if (ms.getCharmNum(1 - player) == 0)
            return;

        sc.funcTargetIn(player, Fields.TRASH);
        sc.setEffect(-2, 0, Motions.GoHand);
    }
}


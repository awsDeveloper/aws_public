using UnityEngine;
using System.Collections;

public class WX08_046 : MonoCard {

	// Use this for initialization
	void Start () {
        sc.AddEffectTemplete(EffectTemplete.triggerType.Cip, cip);

        var bu=sc.AddEffectTemplete(EffectTemplete.triggerType.Burst, burst_1);
        bu.addFuncList(0, burst_2);
        bu.changeCheckStr(0, new string[] { "チャーム", "パワーダウン" });
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void cip()
    {
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.TopSetCharm);
    }

    void burst_1()
    {
        sc.setFieldAllEffect(1 - player, Fields.SIGNIZONE, Motions.TopSetCharm);
    }

    void burst_2()
    {
        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.PowerUpEndPhase);
        sc.powerUpValue = -5000;
    }
}


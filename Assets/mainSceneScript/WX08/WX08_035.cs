using UnityEngine;
using System.Collections;

public class WX08_035 : MonoCard {
    FuncPowerUp fpu;

	// Use this for initialization
	void Start () {
        fpu = gameObject.AddComponent<FuncPowerUp>();
        fpu.setTrueTrigger(2000, alwysCheck);
        fpu.setIsSelfUp();
	
        var he=sc.AddEffectTemplete( EffectTemplete.triggerType.mySigniHeavened,mySigniHevend,true, true);
        he.addEffect(mySigniHevend_2);
	}
	
	// Update is called once per frame
	void Update () {
        fpu.setPUV(2000 * sc.getFuncNum(ms.checkHavingCross, player));
	}

    bool alwysCheck(int x, int target)
    {
        return x == ID && target == player;
    }

    void mySigniHevend()
    {
        sc.setPayCost(cardColorInfo.赤, 2);
    }

    void mySigniHevend_2()
    {
        sc.setEffect(ID, player, Motions.Up);
    }
}


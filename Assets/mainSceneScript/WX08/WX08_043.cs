using UnityEngine;
using System.Collections;

public class WX08_043 : MonoCard {

	// Use this for initialization
	void Start () {
        var c = sc.AddEffectTemplete(EffectTemplete.triggerType.crossCip, cip);
        c.addEffect(cip_2_1);
        c.addFuncList(1, cip_2_2);
        c.changeCheckStr(1, new string[] { "エナゾーン", "ハンド" });
        c.addEffect(cip_3);	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void cip()
    {
        sc.setEffect(0, player, Motions.TopGoShowZone);
        sc.setEffect(0, player, Motions.TopGoShowZone);
    }

    void cip_2_1()
    {
        sc.showZoneTargIn(check);
        sc.setEffect(-1, 0, Motions.GoEnaZone);
    }

    void cip_2_2()
    {
        sc.showZoneTargIn(check);
        sc.setEffect(-1, 0, Motions.GoHand);
    }

    void cip_3()
    {
        sc.setEffect(-1, 0, Motions.ShowZoneGoBottom);
    }

    bool check(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精像_美巧);
    }
}


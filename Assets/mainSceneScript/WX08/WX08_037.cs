using UnityEngine;
using System.Collections;

public class WX08_037 : MonoCard {

	// Use this for initialization
	void Start () {
        var com= gameObject.AddComponent<EffTemManager>();
        com.setTrigger(EffectTemplete.triggerType.crossCip);
        com.setTemplete("バニッシュ", sc.AddEffectTemplete(EffectTemplete.triggerType.crossCip, cip_1));
        com.setTemplete("パワーアップ", sc.AddEffectTemplete(EffectTemplete.triggerType.crossCip, cip_2));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void cip_1()
    {
        sc.setFieldAllEffect(1 - player, Fields.SIGNIZONE, Motions.EnaCharge, check);
    }

    bool check(int x,int target)
    {
        return ms.getCardPower(x, target) <= 3000;
    }


    void cip_2()
    {
        sc.setEffect(ID, player, Motions.changeBaseEnd);
        sc.addParameta(parametaKey.changeBaseValue, 15000);
    }
}


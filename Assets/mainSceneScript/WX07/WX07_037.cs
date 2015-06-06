using UnityEngine;
using System.Collections;

public class WX07_037 : MonoCard {

	// Use this for initialization
	void Start () {
        var com = gameObject.AddComponent<DialogToggle>();
        com.setTrigger(cip);
        com.setAction("パワーアップ", effect_1);
        com.setAction("バニッシュ", effect_2, check_2);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void effect_1()
    {
        sc.setEffect(ID, player, Motions.changeBaseEnd);
        sc.addParameta(parametaKey.changeBaseValue, 15000);
    }

    void effect_2()
    {
        sc.maxPowerBanish(5000);
    }

    bool check_2()
    {
        return sc.getMinPower(1 - player) <= 5000;
    }


    bool cip()
    {
        return sc.CrossingCip();
    }
}


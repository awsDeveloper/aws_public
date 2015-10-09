using UnityEngine;
using System.Collections;

public class WX09_034 : MonoCard {

	// Use this for initialization
	void Start () {
        gameObject.AddComponent<FuncPowerUp>().setForMe(5000, traChe);
        gameObject.AddComponent<FuncPowerUp>().setForMe(5000, sigChe);

        sc.AddEffectTemplete(attTri).addEffect(att);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    bool checkName(int x, int tar)
    {
        return ms.checkType(x, tar, cardTypeInfo.シグニ) && (ms.checkContainsName(x, tar, "パルテノ") || ms.checkContainsName(x, tar, "パルベック"));
    }

    bool traChe()
    {
        return sc.getFuncNum(checkName, player, Fields.TRASH) > 0;
    }
    bool sigChe()
    {
        return sc.getFuncNum(checkName, player, Fields.SIGNIZONE) > 0;
    }

    bool attTri()
    {
        return sc.isAttacking() && ms.getCardPower(ID, player) >= 20000;
    }

    void att()
    {
        sc.setFuncEffect(-1, Motions.EnaCharge, 1 - player, Fields.SIGNIZONE, null);
    }
}


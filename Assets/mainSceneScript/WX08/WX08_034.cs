using UnityEngine;
using System.Collections;

public class WX08_034 : MonoCard {

	// Use this for initialization
	void Start () {
        var at = sc.AddEffectTemplete(EffectTemplete.triggerType.attack, attack);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void attack()
    {
        if (!sc.isFuncOnBatteField(check_arusha, player) || !sc.isFuncOnBatteField(check_korokaro, player))
            return;

        sc.funcTargetIn(1 - player, Fields.SIGNIZONE);
        sc.setEffect(-1, 0, Motions.GoTrash);
    }

    bool check_arusha(int x, int target)
    {
        return ms.checkName(x, target, "羅星　アルシャ");
    }

    bool check_korokaro(int x, int target)
    {
        return ms.checkName(x, target, "羅星　コロカロ");
    }

}


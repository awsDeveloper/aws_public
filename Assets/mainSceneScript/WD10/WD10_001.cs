using UnityEngine;
using System.Collections;

public class WD10_001 : MonoCard{

	// Use this for initialization
	void Start () {
        beforeStart();

        var com = gameObject.AddComponent<FuncPowerUp>();
        com.setTrueTrigger(5000, check);
	}
	
	// Update is called once per frame
	void Update () {
        always();

	
	}

    bool check(int x, int target)
    {
        return ms.checkCross(x, target);
    }

    void always()
    {
        if (!sc.isOnBattleField() || ms.getAttackerID() == -1)
            return;

        if (!ms.getCardScr(ms.getAttackerID() % 50, ms.getAttackerID() / 50).isHeaven())
            return;

        sc.setEffect(0, player, Motions.Draw);
    }
}


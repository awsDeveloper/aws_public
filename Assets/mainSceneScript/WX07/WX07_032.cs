using UnityEngine;
using System.Collections;

public class WX07_032 : MonoCard {
    bool flag = false;
    bool after = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        sc.beforeChantClassDownDownCost(cardClassInfo.精像_悪魔, cardColorInfo.黒, 2);

        if (sc.isChanted())
        {
            sc.funcTargetIn(player, Fields.SIGNIZONE);
            sc.setEffect(-1, 0, Motions.EnaCharge);

            sc.TargetIDEnable = true;
        }

        if (sc.TargetID.Count > 0 && sc.effectTargetID.Count == 0)//自分のシグニをバニッシュしたならば
        {
            int tID=sc.TargetID[0];
            sc.TargetID.RemoveAt(0);
            sc.TargetIDEnable = false;

            sc.Targetable.Clear();

            sc.minPowerBanish(0);

            flag = true;
        }

        if (flag && sc.effectTargetID.Count == 0)
        {
            flag = false;
            sc.Targetable.Clear();

            sc.ClassTargetIn(player, Fields.TRASH, cardClassInfo.精像_悪魔);
            sc.setEffect(-2, 0, Motions.Summon);
            after = true;
        }

        if (after && sc.effectTargetID.Count == 0)
        {
            after = false;
            sc.Targetable.Clear();

            sc.ClassTargetIn(player, Fields.TRASH, cardClassInfo.精像_悪魔);
            sc.setEffect(-2, 0, Motions.TopGoLifeCloth);
        }


        if (sc.isBursted())
            sc.minPowerBanish(0);
	
	}
}


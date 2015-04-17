using UnityEngine;
using System.Collections;

public class SP07_009 : MonoCard{

    bool afterFlag = false;

	// Use this for initialization
	void Start ()
	{
		beforeStart();

	}

	// Update is called once per frame
	void Update ()
	{
        chant();

        after();

        receive();
	}

    void chant()
    {
        if (!sc.isChanted())
            return;

        sc.setEffect(0, player, Motions.TopGoLifeCloth);
        afterFlag = true;
    }

    void after()
    {
        if (!afterFlag || sc.effectTargetID.Count > 0)
            return;

        afterFlag = false;

        sc.funcTargetIn(player, Fields.HAND, check);

        int c = sc.Targetable.Count;
        sc.Targetable.Clear();
            
        sc.DialogFlag = c>=2;
    }

    void receive()
    {
        if (!sc.isMessageYes())
            return;

        sc.funcTargetIn(player, Fields.HAND, check);
        sc.setEffect(-1, 0, Motions.HandDeath);
        sc.setEffect(-1, 0, Motions.HandDeath);
        sc.setEffect(ID, player, Motions.GoLrigDeck);
    }

    bool check(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精像_美巧);
    }
}

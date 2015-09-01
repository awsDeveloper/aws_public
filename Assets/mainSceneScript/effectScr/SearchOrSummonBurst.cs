using UnityEngine;
using System.Collections;

public class SearchOrSummonBurst : MonoCard {
    cardClassInfo info = cardClassInfo.精元;

	// Use this for initialization
	void Start () {
        DialogToggle t = gameObject.AddComponent<DialogToggle>();
        t.setTrigger(DialogToggle.triggerType.Burst);
        t.setAction("サーチ", search);
        t.setAction("サモン", summon);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setMyClass(cardClassInfo myClass)
    {
        info = myClass;
    }

    void search()
    {
        sc.funcTargetIn(player, Fields.MAINDECK, checkClass);
        sc.setEffect(-2, 0, Motions.GoHand);
    }

    void summon()
    {
        sc.funcTargetIn(player, Fields.MAINDECK, checkClass);
        sc.setEffect(-2, 0, Motions.Summon);
    }

    bool checkClass(int x, int target)
    {
        return ms.checkClass(x, target, info);
    }
}


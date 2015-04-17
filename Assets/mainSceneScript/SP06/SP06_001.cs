using UnityEngine;
using System.Collections;

public class SP06_001 : MonoCard{

	// Use this for initialization
	void Start ()
	{
		beforeStart();

        FuncPowerUp fpu = gameObject.AddComponent<FuncPowerUp>();
        fpu.set(2000, trigger);
	}

	// Update is called once per frame
	void Update ()
	{

	}

    bool trigger()
    {
        sc.funcTargetIn(player, Fields.SIGNIZONE, hantei);
        ms.targetableSameNameRemove(sc);

        int count = sc.Targetable.Count;
        sc.Targetable.Clear();

        return count == 3;
    }

    bool hantei(int x, int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精羅_原子);
    }
}

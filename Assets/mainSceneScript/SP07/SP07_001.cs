using UnityEngine;
using System.Collections;

public class SP07_001 : MonoCard{

	// Use this for initialization
	void Start ()
	{
		beforeStart();

        FuncPowerUp f = gameObject.AddComponent<FuncPowerUp>();
        f.setTrueTrigger(1000, check);

        sc.MelhenGrowFlag = true;
	}

	// Update is called once per frame
	void Update ()
	{

	}

    bool check(int x,int target)
    {
        return ms.checkClass(x, target, cardClassInfo.精像_美巧);
    }
}

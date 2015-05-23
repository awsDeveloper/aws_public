using UnityEngine;
using System.Collections;
using System;

public class FuncPowerUp : MonoCard{
    int puv = 2000;

    Func<int,int,bool> check;
    Func<bool> trigger;

	// Use this for initialization
	void Start ()
	{
		beforeStart();

	}

	// Update is called once per frame
	void Update ()
	{
        if (trigger == null || check == null)
            return;

        //check situation
        if (sc.isOnBattleField() && trigger())
        {
            int target = player;
            int f = 3;
            int num = ms.getNumForCard(f, target);

            //check exist in upList
            for (int i = 0; i < num; i++)
            {
                int x = ms.getFieldRankID(f, i, target);

                //requirement add upList
                if (x >= 0 && check(x, target) && ID != x && !ms.checkChanListExist(x, target, ID, player))
                    ms.alwaysChagePower(x, target, puv, ID, player,check);
            }
        }
        else
            ms.powChanListChangerClear(ID, player);

	}

    public void set(int upValue, Func<bool> tri, Func<int, int, bool> che)
    {
        puv = upValue;
        trigger = tri;
        check = che;
    }

    public void set(int upValue, Func<bool> tri)
    {
        set(upValue, tri, nonCheck);
    }

    public void setTrueTrigger(int upValue, Func<int, int, bool> che)
    {
        set(upValue, nonCheck, che);
    }

    bool nonCheck(int x,int target)
    {
        return true;
    }

    bool nonCheck()
    {
        return true;
    }

}

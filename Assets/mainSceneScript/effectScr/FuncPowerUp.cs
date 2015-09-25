using UnityEngine;
using System.Collections;
using System;

public class FuncPowerUp : MonoCard{
    int puv = 2000;
    int myTarget = -1;

    Func<int,int,bool> check;
    Func<bool> trigger;

    bool isSelfUp = false;

    bool isUp = false;
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

        if (isUp)
        {
            isUp = false;
            ms.powChanListChangerClear(ID, player);
        }

        //check situation
        if (sc.isOnBattleField() && trigger())
        {
            isUp = true;

            int target = myTarget;
            int f = 3;
            int num = ms.getNumForCard(f, target);

             //check exist in upList
            for (int i = 0; i < num; i++)
            {
                int x = ms.getFieldRankID(f, i, target);

                //requirement add upList
                if (x >= 0 && check(x, target) && (ID != x || isSelfUp))// && !ms.checkChanListExist(x, target, ID, player))
                    ms.alwaysChagePower(x, target, puv, ID, player,check);
            }

        }
	}

    public void setForMe(int upValue, Func<bool> tri)
    {
        set(upValue, tri, forMe);
        setIsSelfUp();
    }

    public void set(int upValue, Func<bool> tri, Func<int, int, bool> che, int _myTarget=-1)
    {
        puv = upValue;
        trigger = tri;
        check = che;
        myTarget = _myTarget;

        if (myTarget < 0)
            myTarget = sc.player;
    }

    public void set(int upValue, Func<bool> tri)
    {
        set(upValue, tri, nonCheck);
    }

    public void setTrueTrigger(int upValue, Func<int, int, bool> che, int _myTarget=-1)
    {
        set(upValue, nonCheck, che, _myTarget);
    }

    bool nonCheck(int x,int target)
    {
        return true;
    }

    bool nonCheck()
    {
        return true;
    }

    public void setIsSelfUp()
    {
        isSelfUp = true;
    }

    public void setPUV(int x)
    {
        puv = x;
    }

    bool forMe(int x, int target)
    {
        return x == ID && player == target;
    }
}

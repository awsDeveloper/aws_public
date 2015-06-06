using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class classList{
    public classList() { }

    List<cardClassInfo> myClassList = new List<cardClassInfo>();

    public bool checkClass(int x, int target, DeckScript ds)
    {
        for (int i = 0; i < myClassList.Count; i++)
        {
            if (ds.checkClass(x, target, myClassList[i]))
                return true;
        }

        return false;
    }

    public bool checkClassThree(int target, DeckScript ds)
    {
        for (int i = 0; i < myClassList.Count; i++)
        {
            if (ds.getClassNum(target, Fields.SIGNIZONE, myClassList[i]) >= 3)
                return true;
        }

        return false;
    }

    public void setClass(cardClassInfo info)
    {
        myClassList.Add(info);
    }

    public bool isClassSigniOnBattleField(int target,CardScript sc)
    {
        for (int i = 0; i < myClassList.Count; i++)
        {
            if (sc.isClassSigniOnBattleField(target,myClassList[i]))
                return true;
        }

        return false;
    }
}

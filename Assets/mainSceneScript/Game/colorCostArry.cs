using UnityEngine;
using System.Collections;

public class colorCostArry
{
    int[] cost = new int[6];
    int[] downValue;

    public colorCostArry(bool isAllMax)
    {
        ResetDownValue();
        if (isAllMax)
        {
            for (int i = 0; i < cost.Length; i++)
                cost[i] = 100;
        }
    }

    public colorCostArry(int[] a)
    {
        ResetDownValue();
        if (a.Length == cost.Length)
            for (int i = 0; i < cost.Length; i++)
                cost[i] = a[i];
    }

    public colorCostArry(cardColorInfo info, int num)
    {
        ResetDownValue();
        for (int i = 0; i < cost.Length; i++)
            cost[i] = 0;

        cost[(int)info] = num;
    }

    public colorCostArry(colorCostArry orig)
    {
        ResetDownValue();
        for (int i = 0; i < cost.Length; i++)
            cost[i] = orig[i];
    }
    public colorCostArry(colorCostArry c1, colorCostArry c2)
    {
        ResetDownValue();
        for (int i = 0; i < cost.Length; i++)
            cost[i] = c1[i]+c2[i];
    }

    void ResetDownValue()
    {
        downValue = new int[cost.Length];
        for (int i = 0; i < downValue.Length; i++)
            downValue[i] = 0;
    }

    public int getCost(int color)
    {
        return cost[color]-downValue[color];
    }

    public void addCost(int color, int num)
    {
        cost[color] += num;
    }

    public void addCost(cardColorInfo info, int num)
    {
        cost[(int)info] += num;
    }

    public void setDownValue(cardColorInfo info, int num)
    {
        downValue[(int)info] = num;
    }
    public int this[int index]
    {
        get { return getCost(index); }
        set { cost[index] = value; }
    }

    public int Length()
    {
        return cost.Length;
    }

    public int getOriCost(cardColorInfo info)
    {
        return cost[(int)info];
    }

    public bool isEmpty()
    {
        for (int i = 0; i < Length(); i++)
            if (getCost(i) > 0)
                return false;

        return true;
    }
}